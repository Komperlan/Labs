namespace Itmo.CSharpMicroservices.Lab3.Core.Models;

public record struct Product(long Id, string Name, decimal Price, bool IsDeleted);
