namespace Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;

public record CreatedPayload(DateTime CreatedAt, string CreatedBy) : OrderHistoryPayload;