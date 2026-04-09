using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Security;
using Services.Features.Products.Commands.CreateProduct;
using Services.Features.Products.Commands.DeleteProduct;
using Services.Features.Products.Commands.UpdateProduct;
using Services.Features.Products.Queries.GetAllProducts;
using Services.Features.Products.Queries.GetProductById;
using Services.Features.Products.Queries.GetProductByName;
using Services.Features.Products.Queries.GetProductsByCategory;
using Services.Features.Products.Queries.GetProductsByInventory;
using Services.Features.Products.Queries.GetProductsByPriceRange;
using Services.Features.Products.Queries.GetTopProfitableProducts;
using Shared.Requests;

namespace Api.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await sender.Send(new GetAllProductsQuery(pageNumber, pageSize));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetProductByIdQuery(id));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var result = await sender.Send(new GetProductByNameQuery(name));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("category/{categoryId:guid}")]
    public async Task<ActionResult> GetByCategory(Guid categoryId)
    {
        var result = await sender.Send(new GetProductsByCategoryQuery(categoryId));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("inventory/{inventoryId:guid}")]
    public async Task<ActionResult> GetByInventory(Guid inventoryId)
    {
        var result = await sender.Send(new GetProductsByInventoryQuery(inventoryId));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("price-range")]
    public async Task<ActionResult> GetByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        var result = await sender.Send(new GetProductsByPriceRangeQuery(minPrice, maxPrice));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("top-profitable/{count:int}")]
    [Authorize(Roles = $"{SecurityRoles.Admin},{SecurityRoles.Manager}")]
    public async Task<ActionResult> GetTopProfitable(int count)
    {
        var result = await sender.Send(new GetTopProfitableProductsQuery(count));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost]
    [Authorize(Roles = $"{SecurityRoles.Admin},{SecurityRoles.Manager}")]
    public async Task<ActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await sender.Send(new CreateProductCommand(request));
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);

        return BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = $"{SecurityRoles.Admin},{SecurityRoles.Manager}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        var result = await sender.Send(new UpdateProductCommand(id, request));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = SecurityRoles.Admin)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new DeleteProductCommand(id));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }
}
