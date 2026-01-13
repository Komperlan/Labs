using Itmo.CSharpMicroservices.Lab3.Core.Models.PayloadRecords;

namespace Itmo.CSharpMicroservices.Lab3.Core.Models;

public record OrderHistory(long Id, long OrderId, DateTime CreatedAt, OrderHistoryItemKind ItemKind, OrderHistoryPayload ItemPayload);
