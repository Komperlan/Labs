using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.DTO;

public record OrderHistoryDto(long Id, long OrderId, DateTime CreatedAt, OrderHistoryItemKindDTO ItemKind, OrderHistoryPayloadDTO ItemPayload);