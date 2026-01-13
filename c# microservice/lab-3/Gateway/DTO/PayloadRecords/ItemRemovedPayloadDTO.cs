namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;

public record ItemRemovedPayloadDTO(Dictionary<long, int> ProductQuantity) : OrderHistoryPayloadDTO;