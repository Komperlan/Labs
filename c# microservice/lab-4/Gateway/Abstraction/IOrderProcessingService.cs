using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;

public interface IOrderProcessingService
{
    Task ApproveOrderAsync(long orderId, ApproveOrderDto approveOrderDto);

    Task StartPackingAsync(long orderId, StartPackingDto startPackingDto);

    Task FinishPackingAsync(long orderId, FinishPackingDto finishPackingDto);

    Task StartDeliveryAsync(long orderId, StartDeliveryDto startDeliveryDto);

    Task FinishDeliveryAsync(long orderId, FinishDeliveryDto finishDeliveryDto);
}
