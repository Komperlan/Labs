namespace Itmo.CSharpMicroservices.Lab4.Core.Models;

public record Order(long Id, OrderState State, DateTime CreatedAt, string CreatedBy, Dictionary<Product, int> Items);
