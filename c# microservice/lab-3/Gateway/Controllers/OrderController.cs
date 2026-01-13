using Itmo.CSharpMicroservices.Lab3.Gateway.Abstraction;
using Itmo.CSharpMicroservices.Lab3.Gateway.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.CSharpMicroservices.Lab3.Gateway.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        OrderDto order = await _service.CreateOrderAsync(orderDto, cancellationToken);
        return Ok(order);
    }

    [HttpPost("{id}/items/add")]
    public async Task<ActionResult> AddItems(long id, [FromBody] long[] productIds, CancellationToken cancellationToken)
    {
        await _service.AddProductsToOrder(id, productIds, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}/items/remove")]
    public async Task<ActionResult> RemoveItems(long id, [FromBody] long[] productIds, CancellationToken cancellationToken)
    {
        await _service.RemoveProductsFromOrder(id, productIds, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}/processing")]
    public async Task<ActionResult> MarkProcessing(long id, CancellationToken cancellationToken)
    {
        await _service.ChangeOrderStatus(id, OrderStateDTO.Processing, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult> MarkCompleted(long id, CancellationToken cancellationToken)
    {
        await _service.ChangeOrderStatus(id, OrderStateDTO.Completed, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> CancelOrder(long id, CancellationToken cancellationToken)
    {
        await _service.ChangeOrderStatus(id, OrderStateDTO.Cancelled, cancellationToken);

        return Ok();
    }

    [HttpGet("{id}/history")]
    public async Task<ActionResult<List<OrderHistoryDto>>> GetOrderHistory(long id, [FromQuery] long cursor = 0, [FromQuery] int pageSize = 20, [FromQuery] string? kind = null, CancellationToken cancellationToken = default)
    {
        IList<OrderHistoryDto> histories = await _service.GetOrderHistoriesAsync(cursor, pageSize, id, kind == null ? null : Enum.Parse<OrderHistoryItemKindDTO>(kind), cancellationToken);

        return Ok(histories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(long id, CancellationToken cancellationToken)
    {
        OrderDto? order = await _service.FindOrderByIdAsync(id, cancellationToken);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }
}