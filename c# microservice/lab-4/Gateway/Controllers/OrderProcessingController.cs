using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO.OrderProcessingDTO;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Controllers;

[ApiController]
[Route("api/order-processing")]
public class OrderProcessingController : ControllerBase
{
    private readonly IOrderProcessingService _orderProcessingService;

    public OrderProcessingController(IOrderProcessingService orderProcessingService)
    {
        _orderProcessingService = orderProcessingService;
    }

    [HttpPost("{orderId}/approve")]
    public async Task<IActionResult> ApproveOrder(long orderId, [FromBody] ApproveOrderDto approveOrderDto)
    {
        await _orderProcessingService.ApproveOrderAsync(orderId, approveOrderDto);
        return Ok();
    }

    [HttpPost("{orderId}/packing/start")]
    public async Task<IActionResult> StartPacking(long orderId, [FromBody] StartPackingDto startPackingDto)
    {
        await _orderProcessingService.StartPackingAsync(orderId, startPackingDto);
        return Ok();
    }

    [HttpPost("{orderId}/packing/finish")]
    public async Task<IActionResult> FinishPacking(long orderId, [FromBody] FinishPackingDto finishPackingDto)
    {
        await _orderProcessingService.FinishPackingAsync(orderId, finishPackingDto);
        return Ok();
    }

    [HttpPost("{orderId}/delivery/start")]
    public async Task<IActionResult> StartDelivery(long orderId, [FromBody] StartDeliveryDto startDeliveryDto)
    {
        await _orderProcessingService.StartDeliveryAsync(orderId, startDeliveryDto);
        return Ok();
    }

    [HttpPost("{orderId}/delivery/finish")]
    public async Task<IActionResult> FinishDelivery(long orderId, [FromBody] FinishDeliveryDto finishDeliveryDto)
    {
        await _orderProcessingService.FinishDeliveryAsync(orderId, finishDeliveryDto);
        return Ok();
    }
}
