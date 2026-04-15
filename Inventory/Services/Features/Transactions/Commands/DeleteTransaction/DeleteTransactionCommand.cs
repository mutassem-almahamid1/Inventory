using Core.Common;
using Core.Interfaces;
using MediatR;

namespace Services.Features.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(Guid Id) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Transaction-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Transactions-" };
}
