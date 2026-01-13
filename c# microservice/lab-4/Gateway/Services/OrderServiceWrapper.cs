using Google.Protobuf.WellKnownTypes;
using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Services;

public class OrderServiceWrapper : IOrderService
{
    private readonly GrpcOrderService.GrpcOrderServiceClient _grpcClient;

    public OrderServiceWrapper(GrpcOrderService.GrpcOrderServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task AddProductsToOrder(long orderId, long[] products, CancellationToken cancellationToken = default)
    {
        var request = new AddProductsRequest { OrderId = orderId };
        request.ProductIds.AddRange(products);

        OrderResponse response = await _grpcClient.AddProductsToOrderAsync(request);
    }

    public async Task ChangeOrderStatus(long orderId, OrderStateDTO newStatus, CancellationToken cancellationToken = default)
    {
        string statusString = newStatus switch
        {
            OrderStateDTO.Processing => OrderStateDTO.Processing.ToString(),
            OrderStateDTO.Completed => OrderStateDTO.Completed.ToString(),
            OrderStateDTO.Cancelled => OrderStateDTO.Cancelled.ToString(),
            OrderStateDTO.Created => OrderStateDTO.Created.ToString(),
            OrderStateDTO.Approval => OrderStateDTO.Approval.ToString(),
            OrderStateDTO.InDelivery => OrderStateDTO.InDelivery.ToString(),
            OrderStateDTO.Packed => OrderStateDTO.Packed.ToString(),
            OrderStateDTO.Packing => OrderStateDTO.Packing.ToString(),
            _ => throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, "Unknown order status"),
        };
        var request = new ChangeOrderStatusRequest
        {
            OrderId = orderId,
            NewStatus = statusString,
        };

        await _grpcClient.ChangeOrderStatusAsync(request, cancellationToken: cancellationToken);
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto order, CancellationToken cancellationToken = default)
    {
        var request = new CreateOrderRequest { CreatedAt = order.CreatedAt.ToTimestamp(), CreatedBy = order.CreatedBy };
        OrderResponse response = await _grpcClient.CreateOrderAsync(request);
        return OrderMapper.ToOrderDto(response);
    }

    public async Task<OrderDto?> FindOrderByIdAsync(long orderId, CancellationToken cancellationToken = default)
    {
        OrderResponse response = await _grpcClient.GetOrderAsync(new GetOrderRequest { OrderId = orderId });
        return OrderMapper.ToOrderDto(response);
    }

    public async Task<IList<OrderHistoryDto>> GetOrderHistoriesAsync(long cursor, int pageSize, long orderId, OrderHistoryItemKindDTO? kind, CancellationToken cancellationToken = default)
    {
        var request = new GetOrderHistoryRequest { Cursor = cursor, PageSize = pageSize, OrderId = orderId, Kind = kind.ToString() };
        GetOrderHistoryResponse response = await _grpcClient.GetOrderHistoriesAsync(request);
        var list = new List<OrderHistoryDto>();
        foreach (OrderHistoryItem? item in response.Items)
        {
            if (item == null)
            {
                continue;
            }

            list.Add(OrderHistoryMapper.ToOrderHistoryDto(item));
        }

        return list;
    }

    public async Task RemoveProductsFromOrder(long orderId, long[] products, CancellationToken cancellationToken = default)
    {
        var request = new RemoveProductsRequest { OrderId = orderId };
        request.ProductIds.AddRange(products);
        OrderResponse response = await _grpcClient.RemoveProductsFromOrderAsync(request);
    }
}
