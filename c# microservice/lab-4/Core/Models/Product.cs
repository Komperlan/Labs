namespace Itmo.CSharpMicroservices.Lab4.Core.Models;

public record struct Product(long Id, string Name, decimal Price, bool IsDeleted);
