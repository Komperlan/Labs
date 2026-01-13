namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

public record ProductDto(long Id, string Name, decimal Price, bool IsDeleted);