using Itmo.CSharpMicroservices.Lab3.Core.Models;

namespace Itmo.CSharpMicroservices.Lab3.Core.Abstractions;

public interface IOrderHistoryRepository
{
    Task AddOrderHistoryAsync(OrderHistory orderHistory, CancellationToken cancellationToken = default);

    Task<IList<OrderHistory>> GetOrderHistoriesAsync(long cursor, int pageSize, long orderId, OrderHistoryItemKind? kind, CancellationToken cancellationToken = default);
}