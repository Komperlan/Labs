using Google.Protobuf.WellKnownTypes;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Itmo.CSharpMicroservices.Lab4.Core.Models.PayloadRecords;
using Itmo.CSharpMicroservices.Lab4.Grpc;

namespace Itmo.CSharpMicroservices.Lab4.GrpcService.Mappers;

public static class OrderHistoryMapper
{
    public static OrderHistoryItem ToGrpcHistory(this OrderHistory? history)
    {
        ArgumentNullException.ThrowIfNull(history);
        var grpcHistory = new OrderHistoryItem
        {
            HistoryId = history.Id,
            OrderId = history.OrderId,
            CreatedAt = Timestamp.FromDateTime(history.CreatedAt.ToUniversalTime()),
            Kind = history.ItemKind.ToString(),
        };

        switch (history.ItemKind)
        {
            case OrderHistoryItemKind.Created:
                grpcHistory.OrderCreated = new OrderCreatedPayload
                {
                    CreatedBy = ((CreatedPayload)history.ItemPayload).CreatedBy,
                    CreatedAt = Timestamp.FromDateTime(((CreatedPayload)history.ItemPayload).CreatedAt),
                };
                break;

            case OrderHistoryItemKind.ItemAdded:
                grpcHistory.ProductAdded = new ProductAddedPayload();
                long[] productIds = ((ItemAddedPayload)history.ItemPayload).ProductQuantity.Keys.ToArray();
                int[] quantities = ((ItemAddedPayload)history.ItemPayload).ProductQuantity.Values.ToArray();
                grpcHistory.ProductAdded.ProductId.AddRange(productIds);
                grpcHistory.ProductAdded.Quantity.AddRange(quantities);
                break;

            case OrderHistoryItemKind.StateChanged:
                grpcHistory.StateChanged = new StateChangedPayload
                {
                    OldState = ((StatusChangedPayload)history.ItemPayload).OldStatus,
                    NewState = ((StatusChangedPayload)history.ItemPayload).NewStatus,
                };
                break;
            case OrderHistoryItemKind.ItemRemoved:
                grpcHistory.ProductRemoved = new ProductRemovedPayload();
                long[] productId = ((ItemRemovedPayload)history.ItemPayload).ProductQuantity.Keys.ToArray();
                int[] quantity = ((ItemRemovedPayload)history.ItemPayload).ProductQuantity.Values.ToArray();
                grpcHistory.ProductAdded.ProductId.AddRange(productId);
                grpcHistory.ProductAdded.Quantity.AddRange(quantity);
                break;
        }

        return grpcHistory;
    }

    public static GetOrderHistoryResponse ToGetOrderHistoryResponse(this IList<OrderHistory> histories)
    {
        var response = new GetOrderHistoryResponse();

        foreach (OrderHistory history in histories)
        {
            response.Items.Add(ToGrpcHistory(history));
        }

        return response;
    }
}