using Grpc.Core;
using Itmo.CSharpMicroservices.Lab3.Core.Models;
using Itmo.CSharpMicroservices.Lab3.Core.Service;
using Itmo.CSharpMicroservices.Lab3.Grpc;
using Itmo.CSharpMicroservices.Lab3.GrpcService.Mappers;

namespace Itmo.CSharpMicroservices.Lab3.GrpcService.Services;

public class OrderGrpcService : GrpcOrderService.GrpcOrderServiceBase
{
    private readonly OrderService _orderService;

    public OrderGrpcService(OrderService orderService)
    {
        _orderService = orderService;
    }

    public override async Task<OrderResponse> CreateOrder(
        CreateOrderRequest request,
        ServerCallContext context)
    {
        var order = new Order(1, OrderState.Created, request.CreatedAt.ToDateTime(), request.CreatedBy, new Dictionary<Product, int>());

        order = await _orderService.CreateOrderAsync(order, context.CancellationToken);

        return OrderMapper.ToGrpcOrderResponse(order);
    }

    public override async Task<OrderResponse> AddProductsToOrder(
        AddProductsRequest request,
        ServerCallContext context)
    {
        await _orderService.AddProductsToOrder(
            request.OrderId,
            request.ProductIds.ToArray(),
            context.CancellationToken);

        Order? order = await _orderService.FindOrderByIdAsync(request.OrderId, context.CancellationToken);

        return OrderMapper.ToGrpcOrderResponse(order);
    }

    public override async Task<OrderResponse> RemoveProductsFromOrder(
        RemoveProductsRequest request,
        ServerCallContext context)
    {
        await _orderService.RemoveProductsFromOrder(
            request.OrderId,
            request.ProductIds.ToArray(),
            context.CancellationToken);

        Order? order = await _orderService.FindOrderByIdAsync(
            request.OrderId,
            context.CancellationToken);

        return OrderMapper.ToGrpcOrderResponse(order);
    }

    public override async Task<OrderResponse> ChangeOrderStatus(
        ChangeOrderStatusRequest request,
        ServerCallContext context)
    {
        await _orderService.ChangeOrderStatus(
            request.OrderId,
            Enum.Parse<OrderState>(request.NewStatus),
            context.CancellationToken);

        Order? order = await _orderService.FindOrderByIdAsync(
            request.OrderId,
            context.CancellationToken);

        return OrderMapper.ToGrpcOrderResponse(order);
    }

    public override async Task<GetOrderHistoryResponse> GetOrderHistories(
        GetOrderHistoryRequest request,
        ServerCallContext context)
    {
        OrderHistoryItemKind? kind = null;
        if (!string.IsNullOrEmpty(request.Kind))
        {
            kind = Enum.Parse<OrderHistoryItemKind>(request.Kind);
        }

        IList<OrderHistory> histories = await _orderService.GetOrderHistoriesAsync(
            request.Cursor,
            request.PageSize,
            request.OrderId,
            kind,
            context.CancellationToken);

        return OrderHistoryMapper.ToGetOrderHistoryResponse(histories);
    }

    public override async Task<OrderResponse> GetOrder(
        GetOrderRequest request,
        ServerCallContext context)
    {
        Order? order = await _orderService.FindOrderByIdAsync(request.OrderId, context.CancellationToken);

        return OrderMapper.ToGrpcOrderResponse(order);
    }
}