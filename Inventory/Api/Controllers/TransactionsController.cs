using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Transactions.Commands.CreateTransaction;
using Services.Features.Transactions.Commands.DeleteTransaction;
using Services.Features.Transactions.Commands.UpdateTransaction;
using Services.Features.Transactions.Queries.GetAllTransactions;
using Services.Features.Transactions.Queries.GetTransactionById;
using Services.Features.Transactions.Queries.GetTransactionsByEmployee;
using Shared.Requests;

namespace Api.Controllers;

[ApiController]
[Route("api/transactions")]
[Authorize]
public class TransactionsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllTransactionsQuery());
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetTransactionByIdQuery(id));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpGet("employee/{employeeId:guid}")]
    public async Task<ActionResult> GetByEmployee(Guid employeeId)
    {
        var result = await sender.Send(new GetTransactionsByEmployeeQuery(employeeId));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateTransactionRequest request)
    {
        var result = await sender.Send(new CreateTransactionCommand(request));
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);

        return BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateTransactionRequest request)
    {
        var result = await sender.Send(new UpdateTransactionCommand(id, request));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await sender.Send(new DeleteTransactionCommand(id));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }
}
