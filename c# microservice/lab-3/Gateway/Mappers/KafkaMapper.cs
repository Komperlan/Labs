using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;
using Orders.ProcessingService.Contracts;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;

public static class KafkaMapper
{
    public static ApproveOrderRequest ToApproveOrderRequest(ApproveOrderDto approveOrderDto, long orderId)
    {
        return new ApproveOrderRequest
        {
            OrderId = orderId,
            IsApproved = approveOrderDto.IsApproved,
            ApprovedBy = approveOrderDto.ApprovedBy,
            FailureReason = approveOrderDto.FailureReason,
        };
    }

    public static StartOrderPackingRequest ToStartOrderPackingRequest(StartPackingDto startPackingDto, long orderId)
    {
        return new StartOrderPackingRequest
        {
            OrderId = orderId,
            PackingBy = startPackingDto.PackingBy,
        };
    }

    public static FinishOrderPackingRequest ToFinishOrderPackingRequest(FinishPackingDto finishPackingDto, long orderId)
    {
        return new FinishOrderPackingRequest
        {
            OrderId = orderId,
            IsSuccessful = finishPackingDto.IsSuccessful,
            FailureReason = finishPackingDto.FailureReason,
        };
    }

    public static StartOrderDeliveryRequest ToStartOrderDeliveryRequest(StartDeliveryDto startDeliveryDto, long orderId)
    {
        return new StartOrderDeliveryRequest
        {
            OrderId = orderId,
            DeliveredBy = startDeliveryDto.DeliveredBy,
        };
    }

    public static FinishOrderDeliveryRequest ToFinishOrderDeliveryRequest(FinishDeliveryDto finishDeliveryDto, long orderId)
    {
        return new FinishOrderDeliveryRequest
        {
            OrderId = orderId,
            IsSuccessful = finishDeliveryDto.IsSuccessful,
            FailureReason = finishDeliveryDto.FailureReason,
        };
    }
}