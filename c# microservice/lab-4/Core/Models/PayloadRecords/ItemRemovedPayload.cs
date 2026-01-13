namespace Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;

public record ItemRemovedPayload(Dictionary<long, int> ProductQuantity) : OrderHistoryPayload;