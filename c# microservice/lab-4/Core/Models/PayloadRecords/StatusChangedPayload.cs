namespace Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;

public record StatusChangedPayload(string OldStatus, string NewStatus) : OrderHistoryPayload;