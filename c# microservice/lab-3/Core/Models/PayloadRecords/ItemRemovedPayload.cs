namespace Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;

public record ItemRemovedPayload(Dictionary<long, int> ProductQuantity) : OrderHistoryPayload;