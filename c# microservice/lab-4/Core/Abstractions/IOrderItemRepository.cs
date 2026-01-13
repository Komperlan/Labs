using Itmo.CSharpMicroservices.Lab4.Core.Models;

namespace Itmo.CSharpMicroservices.Lab4.Core.Abstractions;

public interface IOrderItemRepository
{
    Task AddOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default);

    Task<OrderItem?> FindOrderItemAsync(long orderId, long productId, CancellationToken cancellationToken = default);

    Task SoftDeleteOrderItemAsync(long orderId, long productId, CancellationToken cancellationToken = default);

    Task<IList<OrderItem>> GetOrderItemsAsync(long cursor, int pageSize, long[]? orderIds, long[]? productIds, bool? isDeleted, CancellationToken cancellationToken = default);

    Task UpdateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default);
}