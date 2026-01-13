using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;

public static class OrderMapper
{
    public static OrderDto ToOrderDto(this OrderResponse response)
    {
        var items = new List<OrderItemDto>();

        foreach (OrderItemMessage? item in response.Items)
        {
            var product = new ProductDto(item.ProductId, item.Name, ProductMapper.ToDecimal(item.Price), item.IsDeleted);
            items.Add(new OrderItemDto(product, item.Quantity));
        }

        var orderDto = new OrderDto(response.Id, Enum.Parse<OrderStateDTO>(response.State), response.CreatedAt.ToDateTime(), response.CreatedBy, items);

        return orderDto;
    }
}