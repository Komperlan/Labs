using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Npgsql;

namespace Itmo.CSharpMicroservices.Lab4.Persistance.Repository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrderItemRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task AddOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            INSERT INTO order_items (order_id, product_id, order_item_quantity, order_item_deleted)
            VALUES (@orderId, @productId, @quantity, false);
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderId", orderItem.OrderId);
        cmd.Parameters.AddWithValue("productId", orderItem.ProductId);
        cmd.Parameters.AddWithValue("quantity", orderItem.ItemQuantity);

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task AddProductsToOrderAsync(long orderId, long[] productIds, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        var productDictionary = productIds.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

        const string selectSql = @"
            SELECT order_item_id, order_item_quantity
            FROM order_items
            WHERE order_id = @orderId AND product_id = @productId;
        ";

        const string insertSql = @"
            INSERT INTO order_items (order_id, product_id, order_item_quantity, order_item_deleted)
            VALUES (@orderId, @productId, @quantity, false);
        ";

        const string updateSql = @"
            UPDATE order_items
            SET order_item_quantity = order_item_quantity + @quantity,
                order_item_deleted = false
            WHERE order_item_id = @orderItemId;
        ";

        await using var selectCmd = new NpgsqlCommand(selectSql, connection);
        selectCmd.Parameters.Add("orderId", NpgsqlTypes.NpgsqlDbType.Bigint);
        selectCmd.Parameters.Add("productId", NpgsqlTypes.NpgsqlDbType.Bigint);

        await using var insertCmd = new NpgsqlCommand(insertSql, connection);
        insertCmd.Parameters.Add("orderId", NpgsqlTypes.NpgsqlDbType.Bigint);
        insertCmd.Parameters.Add("productId", NpgsqlTypes.NpgsqlDbType.Bigint);
        insertCmd.Parameters.Add("quantity", NpgsqlTypes.NpgsqlDbType.Integer);

        await using var updateCmd = new NpgsqlCommand(updateSql, connection);
        updateCmd.Parameters.Add("orderItemId", NpgsqlTypes.NpgsqlDbType.Bigint);
        updateCmd.Parameters.Add("quantity", NpgsqlTypes.NpgsqlDbType.Integer);

        foreach ((long productId, int quantity) in productDictionary)
        {
            selectCmd.Parameters["orderId"].Value = orderId;
            selectCmd.Parameters["productId"].Value = productId;

            long? existingId = null;
            int existingQty = 0;

            await using (NpgsqlDataReader reader = await selectCmd.ExecuteReaderAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    existingId = reader.GetInt64(0);
                    existingQty = reader.GetInt32(1);
                }
            }

            if (existingId is null)
            {
                insertCmd.Parameters["orderId"].Value = orderId;
                insertCmd.Parameters["productId"].Value = productId;
                insertCmd.Parameters["quantity"].Value = quantity;

                await insertCmd.ExecuteNonQueryAsync(cancellationToken);
            }
            else
            {
                updateCmd.Parameters["orderItemId"].Value = existingId.Value;
                updateCmd.Parameters["quantity"].Value = quantity;

                await updateCmd.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        await connection.CloseAsync();
    }

    public async Task RemoveProductsFromOrderAsync(long orderId, long[] productIds, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        var productDictionary = productIds.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

        const string selectSql = @"
            SELECT order_item_id, order_item_quantity
            FROM order_items
            WHERE order_id = @orderId
            AND product_id = @productId
            AND order_item_deleted = false;
        ";

        const string updateSql = @"
            UPDATE order_items
            SET
                order_item_quantity = @newQuantity,
                order_item_deleted  = @deleted
            WHERE order_item_id = @orderItemId;
        ";

        await using var selectCmd = new NpgsqlCommand(selectSql, connection);
        selectCmd.Parameters.Add("orderId", NpgsqlTypes.NpgsqlDbType.Bigint);
        selectCmd.Parameters.Add("productId", NpgsqlTypes.NpgsqlDbType.Bigint);

        await using var updateCmd = new NpgsqlCommand(updateSql, connection);
        updateCmd.Parameters.Add("orderItemId", NpgsqlTypes.NpgsqlDbType.Bigint);
        updateCmd.Parameters.Add("newQuantity", NpgsqlTypes.NpgsqlDbType.Integer);
        updateCmd.Parameters.Add("deleted", NpgsqlTypes.NpgsqlDbType.Boolean);

        foreach ((long productId, int quantityToRemove) in productDictionary)
        {
            selectCmd.Parameters["orderId"].Value = orderId;
            selectCmd.Parameters["productId"].Value = productId;

            long? orderItemId = null;
            int currentQty = 0;

            await using (NpgsqlDataReader reader = await selectCmd.ExecuteReaderAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    orderItemId = reader.GetInt64(0);
                    currentQty = reader.GetInt32(1);
                }
            }

            if (orderItemId is null)
            {
                continue;
            }

            int newQty = currentQty - quantityToRemove;
            bool deleted = newQty <= 0;
            if (newQty < 0)
            {
                newQty = 0;
            }

            updateCmd.Parameters["orderItemId"].Value = orderItemId.Value;
            updateCmd.Parameters["newQuantity"].Value = newQty;
            updateCmd.Parameters["deleted"].Value = deleted;

            await updateCmd.ExecuteNonQueryAsync(cancellationToken);
        }

        await connection.CloseAsync();
    }

    public async Task SoftDeleteOrderItemAsync(long orderId, long productId, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            UPDATE order_items
            SET order_item_deleted = true
            WHERE order_id = @orderId
            AND product_id = @productId;
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderId", orderId);
        cmd.Parameters.AddWithValue("productId", productId);

        await cmd.ExecuteNonQueryAsync(cancellationToken);
        await connection.CloseAsync();
    }

    public async Task<OrderItem?> FindOrderItemAsync(long orderId, long productId, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            SELECT order_item_id, order_id, product_id, order_item_quantity, order_item_deleted
            FROM order_items
            WHERE (order_id = @orderId)
            AND (product_id = @productId);
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderId", orderId);
        cmd.Parameters.AddWithValue("productId", productId);

        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
        {
            return new OrderItem(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetInt64(2),
                reader.GetInt32(3),
                reader.GetBoolean(4));
        }

        await connection.CloseAsync();
        return null;
    }

    public async Task<IList<OrderItem>> GetOrderItemsAsync(long cursor, int pageSize, long[]? orderIds, long[]? productIds, bool? isDeleted, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        string sql = @"
            SELECT order_item_id, order_id, product_id, order_item_quantity, order_item_deleted
            FROM order_items
            WHERE (@orderIds IS NULL OR order_id = ANY(@orderIds))
                AND (@productIds IS NULL OR product_id = ANY(@productIds))
                AND (@isDeleted IS FALSE OR order_item_deleted = @isDeleted)
            ORDER BY order_item_id
            OFFSET @cursor LIMIT @limit;
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderIds", orderIds ?? Array.Empty<long>());
        cmd.Parameters.AddWithValue("productIds", productIds ?? Array.Empty<long>());
        cmd.Parameters.AddWithValue("isDeleted", isDeleted ?? false);
        cmd.Parameters.AddWithValue("cursor", cursor);
        cmd.Parameters.AddWithValue("limit", pageSize);

        var list = new List<OrderItem>();
        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new OrderItem(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetInt64(2),
                reader.GetInt32(3),
                reader.GetBoolean(4)));
        }

        await connection.CloseAsync();

        return list;
    }

    public async Task UpdateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            UPDATE order_items
            SET order_item_quantity = @Quantity,
                order_item_deleted = @IsDeleted
            WHERE order_item_id = @Id;";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("Quantity", orderItem.ItemQuantity);
        cmd.Parameters.AddWithValue("IsDeleted", orderItem.IsDeleted);
        cmd.Parameters.AddWithValue("Id", orderItem.Id);

        await connection.CloseAsync();

        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}