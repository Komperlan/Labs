using Itmo.CSharpMicroservices.Lab3.Core.Models;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.GrpcService.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToProductResponse(Product? product)
    {
        if (!product.HasValue)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var productResponse = new ProductResponse
        {
            ProductId = product.Value.Id,
            Name = product.Value.Name,
            IsDeleted = product.Value.IsDeleted,
            Price = ToDecimalValue(product.Value.Price),
        };

        return productResponse;
    }

    public static DecimalValue ToDecimalValue(decimal value)
    {
        const decimal NanoFactor = 1_000_000_000;
        long units = decimal.ToInt64(value);
        int nanos = decimal.ToInt32((value - units) * NanoFactor);
        return new DecimalValue
        {
            Units = units,
            Nanos = nanos,
        };
    }

    public static decimal ToDecimal(DecimalValue value)
    {
        const decimal NanoFactor = 1_000_000_000;
        return value.Units + (value.Nanos / NanoFactor);
    }

    public static OrderItemMessage ToGrpcOrderItem(KeyValuePair<Product, int> item)
    {
        return new OrderItemMessage
        {
            ProductId = item.Key.Id,
            Quantity = item.Value,
            Name = item.Key.Name,
            Price = ToDecimalValue(item.Key.Price),
        };
    }
}