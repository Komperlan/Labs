using Google.Protobuf.WellKnownTypes;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Itmo.CSharpMicroservices.Lab4.Grpc;

namespace Itmo.CSharpMicroservices.Lab4.GrpcService.Mappers;

public static class OrderMapper
{
    public static OrderResponse ToGrpcOrderResponse(this Order? order)
    {
        ArgumentNullException.ThrowIfNull(order);
        var grpcOrder = new OrderResponse
        {
            Id = order.Id,
            CreatedBy = order.CreatedBy,
            State = order.State.ToString(),
            CreatedAt = Timestamp.FromDateTime(order.CreatedAt.ToUniversalTime()),
        };

        foreach (KeyValuePair<Product, int> item in order.Items)
        {
            grpcOrder.Items.Add(ProductMapper.ToGrpcOrderItem(item));
        }

        return grpcOrder;
    }
}