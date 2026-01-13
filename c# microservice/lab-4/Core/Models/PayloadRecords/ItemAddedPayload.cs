namespace Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;

public record ItemAddedPayload(Dictionary<long, int> ProductQuantity) : OrderHistoryPayload;