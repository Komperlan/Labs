namespace Itmo.CSharpMicroservices.Lab3.Core.Models;

public record struct OrderItem(long Id, long OrderId, long ProductId, int ItemQuantity, bool IsDeleted);
