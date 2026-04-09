using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Security;
using Services.Features.Categories.Commands.CreateCategory;
using Services.Features.Categories.Commands.DeleteCategory;
using Services.Features.Categories.Commands.UpdateCategory;
using Services.Features.Categories.Queries.GetAllCategories;
using Services.Features.Categories.Queries.GetCategoryById;
using Services.Features.Categories.Queries.GetCategoryByName;
using Shared.Requests;

namespace Api.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await sender.Send(new GetAllCategoriesQuery(pageNumber, pageSize));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetCategoryByIdQuery(id));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var result = await sender.Send(new GetCategoryByNameQuery(name));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpPost]
    [Authorize(Roles = $"{SecurityRoles.Admin},{SecurityRoles.Manager}")]
    public async Task<ActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var result = await sender.Send(new CreateCategoryCommand(request));
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);

        return BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = $"{SecurityRoles.Admin},{SecurityRoles.Manager}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var result = await sender.Send(new UpdateCategoryCommand(id, request));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = SecurityRoles.Admin)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new DeleteCategoryCommand(id));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }
}
