using Itmo.CSharpMicroservices.Lab3.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab3.Core.Models;
using Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;

namespace Itmo.CSharpMicroservices.Lab3.Persistance.Repository;

public class OrderHistoryRepository : IOrderHistoryRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrderHistoryRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task AddOrderHistoryAsync(OrderHistory orderHistory, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            INSERT INTO order_history (order_id, order_history_item_created_at, order_history_item_kind, order_history_item_payload)
            VALUES (@orderId, NOW(), @kind::order_history_item_kind, @payload::jsonb);
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderId", orderHistory.OrderId);
        cmd.Parameters.AddWithValue("kind", orderHistory.ItemKind.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));
        cmd.Parameters.AddWithValue("payload", JsonSerializer.Serialize(orderHistory.ItemPayload));

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        await connection.CloseAsync();
    }

    public async Task<IList<OrderHistory>> GetOrderHistoriesAsync(long cursor, int pageSize, long orderId, OrderHistoryItemKind? kind, CancellationToken cancellationToken = default)
    {
        await using NpgsqlConnection connection = _dataSource.OpenConnection();

        const string sql = @"
            SELECT order_history_item_id, order_id, order_history_item_created_at, order_history_item_kind::text, order_history_item_payload
            FROM order_history
            WHERE order_id = @orderId
                AND (@kind IS NULL OR order_history_item_kind::text = @kind)
            ORDER BY order_history_item_created_at DESC
            OFFSET @offset LIMIT @limit;
        ";

        await using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("orderId", orderId);

        if (kind != null)
        {
            cmd.Parameters.AddWithValue("kind", kind.Value.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));
        }
        else
        {
            NpgsqlParameter param = cmd.Parameters.Add("kind", NpgsqlDbType.Varchar);
            param.Value = DBNull.Value;
        }

        cmd.Parameters.AddWithValue("offset", cursor);
        cmd.Parameters.AddWithValue("limit", pageSize);

        var list = new List<OrderHistory>();

        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            string info = reader.GetString(4);
            OrderHistoryPayload? payload = JsonSerializer.Deserialize<OrderHistoryPayload>(info);
            ArgumentNullException.ThrowIfNull(payload);
            list.Add(new OrderHistory(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetDateTime(2),
                Enum.Parse<OrderHistoryItemKind>(reader.GetString(3), ignoreCase: true),
                payload));
        }

        await connection.CloseAsync();

        return list;
    }
}