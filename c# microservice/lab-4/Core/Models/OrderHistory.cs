using Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;

namespace Itmo.CSharpMicroservices.Lab4.Core.Models;

public record OrderHistory(long Id, long OrderId, DateTime CreatedAt, OrderHistoryItemKind ItemKind, OrderHistoryPayload ItemPayload);
