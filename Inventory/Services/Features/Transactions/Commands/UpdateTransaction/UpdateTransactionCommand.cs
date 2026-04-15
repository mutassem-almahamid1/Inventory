using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(Guid Id, UpdateTransactionRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Transaction-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Transactions-" };
}
