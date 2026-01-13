using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;
using Itmo.CSharpMicroservices.Lab3.Gateway.Mappers;
using Microsoft.AspNetCore.Mvc;
using Orders.ProcessingService.Contracts;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Controllers;

[ApiController]
[Route("api/order-processing")]
public class OrderProcessingController : ControllerBase
{
    private readonly OrderService.OrderServiceClient _grpcClient;

    public OrderProcessingController(OrderService.OrderServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    [HttpPost("{orderId}/approve")]
    public async Task<IActionResult> ApproveOrder(long orderId, [FromBody] ApproveOrderDto approveOrderDto)
    {
        await _grpcClient.ApproveOrderAsync(KafkaMapper.ToApproveOrderRequest(approveOrderDto, orderId));
        return Ok();
    }

    [HttpPost("{orderId}/packing/start")]
    public async Task<IActionResult> StartPacking(long orderId, [FromBody] StartPackingDto startPackingDto)
    {
        await _grpcClient.StartOrderPackingAsync(KafkaMapper.ToStartOrderPackingRequest(startPackingDto, orderId));
        return Ok();
    }

    [HttpPost("{orderId}/packing/finish")]
    public async Task<IActionResult> FinishPacking(long orderId, [FromBody] FinishPackingDto finishPackingDto)
    {
        await _grpcClient.FinishOrderPackingAsync(KafkaMapper.ToFinishOrderPackingRequest(finishPackingDto, orderId));
        return Ok();
    }

    [HttpPost("{orderId}/delivery/start")]
    public async Task<IActionResult> StartDelivery(long orderId, [FromBody] StartDeliveryDto startDeliveryDto)
    {
        await _grpcClient.StartOrderDeliveryAsync(KafkaMapper.ToStartOrderDeliveryRequest(startDeliveryDto, orderId));
        return Ok();
    }

    [HttpPost("{orderId}/delivery/finish")]
    public async Task<IActionResult> FinishDelivery(long orderId, [FromBody] FinishDeliveryDto finishDeliveryDto)
    {
        await _grpcClient.FinishOrderDeliveryAsync(KafkaMapper.ToFinishOrderDeliveryRequest(finishDeliveryDto, orderId));
        return Ok();
    }
}
