namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;

public record ItemAddedPayloadDTO(Dictionary<long, int> ProductQuantity) : OrderHistoryPayloadDTO;