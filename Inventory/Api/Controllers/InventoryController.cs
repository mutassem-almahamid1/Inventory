using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Inventory.Commands.CreateInventory;
using Services.Features.Inventory.Commands.DeleteInventory;
using Services.Features.Inventory.Commands.UpdateInventory;
using Services.Features.Inventory.Commands.UpdateInventoryProductQuantity;
using Services.Features.Inventory.Queries.GetAllInventory;
using Services.Features.Inventory.Queries.GetInventoryById;
using Services.Features.Inventory.Queries.GetInventoryByName;
using Services.Features.Inventory.Queries.GetLowStockInventory;
using Services.Features.Products.Queries.GetProductsByInventory;
using Shared.Requests;

namespace Api.Controllers;

[ApiController]
[Route("api/inventories")]
[Authorize]
public class InventoryController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllInventoryQuery());
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetInventoryByIdQuery(id));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var result = await sender.Send(new GetInventoryByNameQuery(name));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("low-stock")]
    public async Task<ActionResult> GetLowStock()
    {
        var result = await sender.Send(new GetLowStockInventoryQuery());
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("{inventoryId:guid}/products")]
    public async Task<ActionResult> GetProductsByInventory(Guid inventoryId)
    {
        var result = await sender.Send(new GetProductsByInventoryQuery(inventoryId));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateInventoryRequest request)
    {
        var result = await sender.Send(new CreateInventoryCommand(request));
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);

        return BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateInventoryRequest request)
    {
        var result = await sender.Send(new UpdateInventoryCommand(id, request));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpPut("{inventoryId:guid}/products/{productId:guid}/quantity")]
    public async Task<ActionResult> UpdateProductQuantity(Guid inventoryId, Guid productId, [FromBody] UpdateInventoryProductQuantityRequest request)
    {
        var result = await sender.Send(new UpdateInventoryProductQuantityCommand(inventoryId, productId, request));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new DeleteInventoryCommand(id));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }
}
