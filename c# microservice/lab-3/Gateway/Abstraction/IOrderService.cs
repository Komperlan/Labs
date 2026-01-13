using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(OrderDto order, CancellationToken cancellationToken = default);

    Task<OrderDto?> FindOrderByIdAsync(long orderId, CancellationToken cancellationToken = default);

    Task AddProductsToOrder(long orderId, long[] products, CancellationToken cancellationToken = default);

    Task RemoveProductsFromOrder(long orderId, long[] products, CancellationToken cancellationToken = default);

    Task ChangeOrderStatus(long orderId, OrderStateDTO newStatus, CancellationToken cancellationToken = default);

    Task<IList<OrderHistoryDto>> GetOrderHistoriesAsync(long cursor, int pageSize, long orderId, OrderHistoryItemKindDTO? kind, CancellationToken cancellationToken = default);
}