namespace Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;

public record StatusChangedPayload(string OldStatus, string NewStatus) : OrderHistoryPayload;