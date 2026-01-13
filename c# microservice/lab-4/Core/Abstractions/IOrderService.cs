using Itmo.CSharpMicroservices.Lab4.Core.Models;

namespace Itmo.CSharpMicroservices.Lab4.Core.Abstractions;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> FindOrderByIdAsync(long orderId, CancellationToken cancellationToken = default);

    Task AddProductsToOrder(long orderId, long[] products, CancellationToken cancellationToken = default);

    Task RemoveProductsFromOrder(long orderId, long[] products, CancellationToken cancellationToken = default);

    Task ChangeOrderStatus(long orderId, OrderState newStatus, CancellationToken cancellationToken = default);

    Task<IList<OrderHistory>> GetOrderHistoriesAsync(long cursor, int pageSize, long orderId, OrderHistoryItemKind? kind, CancellationToken cancellationToken = default);
}