namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

public record OrderItemDto(ProductDto Product, int Quantity);