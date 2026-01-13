namespace Itmo.CSharpMicroservices.Lab4.Core.Models;

public record struct OrderItem(long Id, long OrderId, long ProductId, int ItemQuantity, bool IsDeleted);
