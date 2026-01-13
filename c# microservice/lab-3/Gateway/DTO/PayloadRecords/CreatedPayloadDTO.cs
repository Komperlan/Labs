namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;

public record CreatedPayloadDTO(DateTime CreatedAt, string CreatedBy) : OrderHistoryPayloadDTO;