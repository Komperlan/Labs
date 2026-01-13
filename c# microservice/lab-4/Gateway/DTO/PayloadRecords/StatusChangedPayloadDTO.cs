namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;

public record StatusChangedPayloadDTO(string OldStatus, string NewStatus) : OrderHistoryPayloadDTO;