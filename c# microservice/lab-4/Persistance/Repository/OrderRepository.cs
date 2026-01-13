using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Npgsql;

namespace Itmo.CSharpMicroservices.Lab4.Persistance.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrderRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            INSERT INTO orders (order_state, order_created_at, order_created_by)
            VALUES (@orderStatus::order_state, @createdAt, @createdBy)
            RETURNING order_id;
        ";

        const string itemSql = @"
            INSERT INTO order_items (order_id, product_id, quantity)
            VALUES (@orderId, @productId, @quantity, FALSE);";

        await using var cmd = new NpgsqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("createdBy", order.CreatedBy);
        cmd.Parameters.AddWithValue("createdAt", order.CreatedAt);
        cmd.Parameters.AddWithValue("orderStatus", order.State.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));

        object? result = await cmd.ExecuteScalarAsync(cancellationToken);
        ArgumentNullException.ThrowIfNull(result);

        if (order.Items.Count != 0)
        {
            foreach ((Product product, int quantity) in order.Items)
            {
                await using var itemCmd = new NpgsqlCommand(itemSql, connection);
                itemCmd.Parameters.AddWithValue("orderId", (long)result);
                itemCmd.Parameters.AddWithValue("productId", product.Id);
                itemCmd.Parameters.AddWithValue("quantity", quantity);
                await itemCmd.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        await connection.CloseAsync();

        return new Order((long)result, order.State, order.CreatedAt, order.CreatedBy, order.Items);
    }

    public async Task UpdateOrderStateAsync(long orderId, OrderState newState, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            UPDATE orders
            SET order_state = @orderStatus::order_state
            WHERE order_id = @orderId;
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderStatus", newState.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));
        cmd.Parameters.AddWithValue("orderId", orderId);

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IList<Order>> GetOrdersAsync(long cursor, int pageSize, long[]? ids, OrderState[]? states, string? createdBy, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        string[] stateStrings = states?.Select(s => s.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture)).ToArray() ?? Array.Empty<string>();

        const string sql = @"
            SELECT order_id, order_state::text, order_created_at, order_created_by FROM orders
            WHERE (@ids IS NULL OR order_id = ANY(@ids))
                AND (@states IS NULL OR order_state::text = ANY(@states))
                AND (@createdBy IS NULL OR order_created_by = @createdBy)
            ORDER BY order_id
            OFFSET @cursor LIMIT @limit;
        ";

        const string itemsSql = @"
                SELECT oi.product_id, oi.order_item_quantity, oi.order_item_deleted,
                       p.product_name, p.product_price, p.product_deleted, oi.order_id
                FROM order_items oi
                JOIN products p ON oi.product_id = p.product_id
                WHERE oi.order_id = @orderId";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("ids", ids ?? Array.Empty<long>());
        cmd.Parameters.AddWithValue("states", stateStrings);
        cmd.Parameters.AddWithValue("createdBy", (object?)createdBy ?? DBNull.Value);
        cmd.Parameters.AddWithValue("cursor", cursor);
        cmd.Parameters.AddWithValue("limit", pageSize);

        var list = new List<Order>();
        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var order = new Order(
                reader.GetInt64(0),
                Enum.Parse<OrderState>(reader.GetString(1), ignoreCase: true),
                reader.GetDateTime(2),
                reader.GetString(3),
                new Dictionary<Product, int>());
            list.Add(order);
        }

        reader.Close();

        await using var itemCmd = new NpgsqlCommand(itemsSql, connection);

        using NpgsqlDataReader itemReader = await itemCmd.ExecuteReaderAsync(cancellationToken);

        while (await itemReader.ReadAsync(cancellationToken))
        {
            Order? order = list.Find(x => x.Id == itemReader.GetInt64(6));
            if (!itemReader.GetBoolean(2))
            {
                var product = new Product(itemReader.GetInt64(0), itemReader.GetString(3), itemReader.GetDecimal(4), itemReader.GetBoolean(5));
                order?.Items.Add(product, itemReader.GetInt32(1));
            }
        }

        await connection.CloseAsync();

        return list;
    }

    public async Task<Order?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            SELECT order_id, order_state::text, order_created_at, order_created_by FROM orders
            WHERE (order_id = @id)
        ";

        const string itemsSql = @"
                SELECT oi.product_id, oi.order_item_quantity, oi.order_item_deleted,
                       p.product_name, p.product_price, p.product_deleted, oi.order_id
                FROM order_items oi JOIN products p ON oi.product_id = p.product_id
                WHERE oi.order_id = @orderId";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("id", id);

        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

        Order? order = null;

        if (await reader.ReadAsync(cancellationToken))
        {
            order = new Order(
                reader.GetInt64(0),
                Enum.Parse<OrderState>(reader.GetString(1), ignoreCase: true),
                reader.GetDateTime(2),
                reader.GetString(3),
                new Dictionary<Product, int>());
        }

        if (order == null)
        {
            return null;
        }

        reader.Close();

        await using var itemCmd = new NpgsqlCommand(itemsSql, connection);
        itemCmd.Parameters.AddWithValue("orderId", order.Id);

        await using NpgsqlDataReader itemReader = await itemCmd.ExecuteReaderAsync(cancellationToken);

        while (await itemReader.ReadAsync(cancellationToken))
        {
            if (!itemReader.GetBoolean(2))
            {
                var product = new Product(itemReader.GetInt64(0), itemReader.GetString(3), itemReader.GetDecimal(4), itemReader.GetBoolean(5));
                order.Items.Add(product, itemReader.GetInt32(1));
            }
        }

        await connection.CloseAsync();

        return order;
    }
}