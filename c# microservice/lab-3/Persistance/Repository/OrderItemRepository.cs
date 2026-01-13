using Itmo.CSharpMicroservices.Lab3.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab3.Core.Models;
using Npgsql;

namespace Itmo.CSharpMicroservices.Lab3.Persistance.Repository;

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

        const string sql = @"
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

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        await connection.CloseAsync();
    }
}