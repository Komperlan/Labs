using Google.Protobuf.WellKnownTypes;
using Itmo.CSharpMicroservices.Lab3.Core.Models;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.GrpcService.Mappers;

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