using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;
using Itmo.CSharpMicroservices.Lab4.Core.Wrapper;
using System.Transactions;

namespace Itmo.CSharpMicroservices.Lab4.Core.Service;

public class OrderService : IOrderService
{
    private readonly IOrderHistoryRepository _orderHistoryRepository;

    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    private readonly ProducerWrapper _producer;

    public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IOrderHistoryRepository orderHistoryRepository, ProducerWrapper producer)
    {
        _orderHistoryRepository = orderHistoryRepository;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _producer = producer;
    }

    public async Task<Order> CreateOrderAsync(Order order,  CancellationToken cancellationToken = default)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Order returnOrder = await _orderRepository.CreateOrderAsync(order);
        var history = new OrderHistory(1, returnOrder.Id, DateTime.Now, OrderHistoryItemKind.Created, new CreatedPayload(order.CreatedAt, order.CreatedBy));
        await _orderHistoryRepository.AddOrderHistoryAsync(history);
        scope.Complete();

        await _producer.ProduceOrderCreated(returnOrder);

        return returnOrder;
    }

    public async Task<Order?> FindOrderByIdAsync(long orderId,  CancellationToken cancellationToken = default)
    {
        Order? order = await _orderRepository.FindByIdAsync(orderId);
        return order;
    }

    public async Task AddProductsToOrder(long orderId,  long[] products, CancellationToken cancellationToken = default)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Order? order = await _orderRepository.FindByIdAsync(orderId, cancellationToken);
        ArgumentNullException.ThrowIfNull(order);
        var productDictionary = products.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        var payload = new ItemRemovedPayload(productDictionary);

        foreach (long productId in products)
        {
            OrderItem? orderItem = await _orderItemRepository.FindOrderItemAsync(orderId, productId, cancellationToken);
            if (orderItem == null)
            {
                continue;
            }

            int delta = orderItem.Value.ItemQuantity + productDictionary[orderItem.Value.ProductId];

            var newOrderItem = new OrderItem(orderItem.Value.Id, orderId, orderItem.Value.ProductId, delta, orderItem.Value.IsDeleted);

            await _orderItemRepository.UpdateOrderItemAsync(newOrderItem, cancellationToken);
        }

        await _orderHistoryRepository.AddOrderHistoryAsync(new OrderHistory(1, orderId, DateTime.Now, OrderHistoryItemKind.ItemRemoved, payload), cancellationToken);
        scope.Complete();
    }

    public async Task RemoveProductsFromOrder(long orderId, long[] products,  CancellationToken cancellationToken = default)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Order? order = await _orderRepository.FindByIdAsync(orderId, cancellationToken);
        ArgumentNullException.ThrowIfNull(order);
        var productDictionary = products.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        var payload = new ItemRemovedPayload(productDictionary);

        foreach (long productId in products)
        {
            OrderItem? orderItem = await _orderItemRepository.FindOrderItemAsync(orderId, productId, cancellationToken);
            if (orderItem == null)
            {
                continue;
            }

            int delta = Math.Max(0, orderItem.Value.ItemQuantity - productDictionary[orderItem.Value.ProductId]);

            var newOrderItem = new OrderItem(orderItem.Value.Id, orderId, orderItem.Value.ProductId, delta, delta == 0);

            await _orderItemRepository.UpdateOrderItemAsync(newOrderItem, cancellationToken);
        }

        await _orderHistoryRepository.AddOrderHistoryAsync(new OrderHistory(1, orderId, DateTime.Now, OrderHistoryItemKind.ItemRemoved, payload), cancellationToken);
        scope.Complete();
    }

    public async Task ChangeOrderStatus(long orderId, OrderState newStatus, CancellationToken cancellationToken = default)
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Order? order = await _orderRepository.FindByIdAsync(orderId, cancellationToken);
        ArgumentNullException.ThrowIfNull(order);
        OrderState oldStatus = order.State;

        if (newStatus == OrderState.Cancelled && oldStatus != OrderState.Created)
        {
            throw new ArgumentException("Only created order can be canceled");
        }

        if (newStatus == OrderState.Processing)
        {
            await _producer.ProduceOrderProcessed(order);
        }

        await _orderRepository.UpdateOrderStateAsync(orderId, newStatus, cancellationToken);
        var payload = new StatusChangedPayload(oldStatus.ToString(), newStatus.ToString());
        var history = new OrderHistory(0, orderId, DateTime.Now, OrderHistoryItemKind.StateChanged, payload);
        await _orderHistoryRepository.AddOrderHistoryAsync(history, cancellationToken);

        scope.Complete();
    }

    public async Task<IList<OrderHistory>> GetOrderHistoriesAsync(long cursor, int pageSize, long orderId, OrderHistoryItemKind? kind, CancellationToken cancellationToken = default)
    {
        return await _orderHistoryRepository.GetOrderHistoriesAsync(cursor, pageSize, orderId, kind, cancellationToken);
    }
}
