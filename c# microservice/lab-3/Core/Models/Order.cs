namespace Itmo.CSharpMicroservices.Lab3.Core.Models;

public record Order(long Id, OrderState State, DateTime CreatedAt, string CreatedBy, Dictionary<Product, int> Items);
