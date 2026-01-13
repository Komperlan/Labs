using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.PayloadRecords;
using Itmo.CSharpMicroservices.Lab3.Grpc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;

public static class OrderHistoryMapper
{
    public static OrderHistoryDto ToOrderHistoryDto(OrderHistoryItem item)
    {
        OrderHistoryItemKindDTO kind = Enum.Parse<OrderHistoryItemKindDTO>(item.Kind);
        OrderHistoryPayloadDTO payload = kind switch
        {
            OrderHistoryItemKindDTO.Created => new CreatedPayloadDTO(item.OrderCreated.CreatedAt.ToDateTime(), item.OrderCreated.CreatedBy),

            OrderHistoryItemKindDTO.ItemAdded => new ItemAddedPayloadDTO(item.ProductAdded.ProductId.Zip(item.ProductAdded.Quantity, (id, qty) => new { id, qty }).ToDictionary(x => x.id, x => x.qty)),
            OrderHistoryItemKindDTO.ItemRemoved => new ItemRemovedPayloadDTO(item.ProductRemoved.ProductId.Zip(item.ProductRemoved.Quantity, (id, qty) => new { id, qty }).ToDictionary(x => x.id, x => x.qty)),
            OrderHistoryItemKindDTO.StateChanged => new StatusChangedPayloadDTO(item.StateChanged.OldState, item.StateChanged.NewState),
            _ => new StatusChangedPayloadDTO(item.StateChanged.OldState, item.StateChanged.NewState),
        };
        var history = new OrderHistoryDto(item.HistoryId, item.OrderId, item.CreatedAt.ToDateTime(), kind, payload);

        return history;
    }
}