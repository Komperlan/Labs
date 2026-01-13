using Itmo.CSharpMicroservices.Lab3.Core.Models;

namespace Itmo.CSharpMicroservices.Lab3.Core.Abstractions;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);

    Task UpdateOrderStateAsync(long orderId, OrderState newState, CancellationToken cancellationToken = default);

    Task<IList<Order>> GetOrdersAsync(long cursor, int pageSize, long[]? ids, OrderState[]? states, string? createdBy, CancellationToken cancellationToken = default);

    Task<Order?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
}