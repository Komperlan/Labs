namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

public record OrderDto(long Id, OrderStateDTO State, DateTime CreatedAt, string CreatedBy,  IList<OrderItemDto> Items);