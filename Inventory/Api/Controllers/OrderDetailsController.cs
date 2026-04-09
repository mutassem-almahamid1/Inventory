using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.OrderDetails.Commands.CreateOrderDetail;
using Services.Features.OrderDetails.Commands.DeleteOrderDetail;
using Services.Features.OrderDetails.Commands.UpdateOrderDetail;
using Services.Features.OrderDetails.Queries.GetAllOrderDetails;
using Services.Features.OrderDetails.Queries.GetOrderDetailById;
using Services.Features.OrderDetails.Queries.GetOrderDetailsByOrderId;
using Shared.Requests;

namespace Api.Controllers;

[ApiController]
[Route("api/order-details")]
[Authorize]
public class OrderDetailsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllOrderDetailsQuery());
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetOrderDetailByIdQuery(id));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<ActionResult> GetByOrderId(Guid orderId)
    {
        var result = await sender.Send(new GetOrderDetailsByOrderIdQuery(orderId));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderDetailRequest request)
    {
        var result = await sender.Send(new CreateOrderDetailCommand(request));
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);

        return BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateOrderDetailRequest request)
    {
        var result = await sender.Send(new UpdateOrderDetailCommand(id, request));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new DeleteOrderDetailCommand(id));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }
}
