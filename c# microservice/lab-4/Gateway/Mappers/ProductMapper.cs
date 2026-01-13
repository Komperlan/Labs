using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;

public static class ProductMapper
{
    public static decimal ToDecimal(this DecimalValue value)
    {
        const decimal NanoFactor = 1_000_000_000;
        return value.Units + (value.Nanos / NanoFactor);
    }

    public static DecimalValue ToDecimalValue(this decimal value)
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

    public static ProductDto ToProductDto(this ProductResponse response)
    {
        return new ProductDto(response.ProductId, response.Name, ToDecimal(response.Price), response.IsDeleted);
    }
}