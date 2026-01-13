namespace Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;

public record ItemAddedPayload(Dictionary<long, int> ProductQuantity) : OrderHistoryPayload;