using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;
using Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;
using Orders.ProcessingService.Contracts;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Services;

public class OrderProcessingService : IOrderProcessingService
{
    private readonly OrderService.OrderServiceClient _grpcClient;

    public OrderProcessingService(OrderService.OrderServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task ApproveOrderAsync(long orderId, ApproveOrderDto approveOrderDto)
    {
        await _grpcClient.ApproveOrderAsync(KafkaMapper.ToApproveOrderRequest(approveOrderDto, orderId));
    }

    public async Task StartPackingAsync(long orderId, StartPackingDto startPackingDto)
    {
        await _grpcClient.StartOrderPackingAsync(KafkaMapper.ToStartOrderPackingRequest(startPackingDto, orderId));
    }

    public async Task FinishPackingAsync(long orderId, FinishPackingDto finishPackingDto)
    {
        await _grpcClient.FinishOrderPackingAsync(KafkaMapper.ToFinishOrderPackingRequest(finishPackingDto, orderId));
    }

    public async Task StartDeliveryAsync(long orderId, StartDeliveryDto startDeliveryDto)
    {
        await _grpcClient.StartOrderDeliveryAsync(KafkaMapper.ToStartOrderDeliveryRequest(startDeliveryDto, orderId));
    }

    public async Task FinishDeliveryAsync(long orderId, FinishDeliveryDto finishDeliveryDto)
    {
        await _grpcClient.FinishOrderDeliveryAsync(KafkaMapper.ToFinishOrderDeliveryRequest(finishDeliveryDto, orderId));
    }
}