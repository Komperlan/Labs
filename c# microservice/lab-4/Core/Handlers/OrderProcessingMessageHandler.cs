using Confluent.Kafka;
using Itmo.CSharpMicroservices.Lab4.Core.Abstractions;
using Itmo.CSharpMicroservices.Lab4.Core.Models;
using Itmo.CSharpMicroservices.Lab4.Kafka.Abstractions;
using Orders.Kafka.Contracts;

namespace Itmo.CSharpMicroservices.Lab4.Core.Handler;

public class OrderProcessingMessageHandler : IKafkaMessageHandler<OrderProcessingKey, OrderProcessingValue>
{
    private readonly IOrderService _orderService;

    public OrderProcessingMessageHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task HandleAsync(
        IReadOnlyCollection<ConsumeResult<OrderProcessingKey, OrderProcessingValue>> messages,
        CancellationToken cancellationToken)
    {
        foreach (ConsumeResult<OrderProcessingKey, OrderProcessingValue> message in messages)
        {
            long orderId = message.Message.Key.OrderId;
            OrderProcessingValue value = message.Message.Value;

            switch (value.EventCase)
            {
                case OrderProcessingValue.EventOneofCase.ApprovalReceived:
                    await HandleApprovalReceived(value.ApprovalReceived);
                    break;
                case OrderProcessingValue.EventOneofCase.PackingStarted:
                    await HandlePackingStarted(value.PackingStarted);
                    break;
                case OrderProcessingValue.EventOneofCase.DeliveryStarted:
                    await HandleDeliveryStarted(value.DeliveryStarted);
                    break;
                case OrderProcessingValue.EventOneofCase.PackingFinished:
                    await HandlePackingFinished(value.PackingFinished);
                    break;

                case OrderProcessingValue.EventOneofCase.DeliveryFinished:
                    await HandleDeliveryFinished(value.DeliveryFinished);
                    break;
            }
        }
    }

    private async Task HandleApprovalReceived(OrderProcessingValue.Types.OrderApprovalReceived approval)
    {
        if (!approval.IsApproved)
        {
            await _orderService.ChangeOrderStatus(approval.OrderId, OrderState.Cancelled);
        }

        await _orderService.ChangeOrderStatus(approval.OrderId, OrderState.Approval);
    }

    private async Task HandlePackingFinished(OrderProcessingValue.Types.OrderPackingFinished packing)
    {
        if (packing.IsFinishedSuccessfully)
        {
            await _orderService.ChangeOrderStatus(packing.OrderId, OrderState.Packed);
            return;
        }

        await _orderService.ChangeOrderStatus(packing.OrderId, OrderState.Cancelled);
    }

    private async Task HandlePackingStarted(OrderProcessingValue.Types.OrderPackingStarted packing)
    {
        await _orderService.ChangeOrderStatus(packing.OrderId, OrderState.Packing);
    }

    private async Task HandleDeliveryFinished(OrderProcessingValue.Types.OrderDeliveryFinished delivery)
    {
        if (delivery.IsFinishedSuccessfully)
        {
            await _orderService.ChangeOrderStatus(delivery.OrderId, OrderState.Completed);
            return;
        }

        await _orderService.ChangeOrderStatus(delivery.OrderId, OrderState.Cancelled);
    }

    private async Task HandleDeliveryStarted(OrderProcessingValue.Types.OrderDeliveryStarted delivery)
    {
        await _orderService.ChangeOrderStatus(delivery.OrderId, OrderState.InDelivery);
    }
}
