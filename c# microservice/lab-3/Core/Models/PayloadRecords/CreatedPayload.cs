namespace Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;

public record CreatedPayload(DateTime CreatedAt, string CreatedBy) : OrderHistoryPayload;