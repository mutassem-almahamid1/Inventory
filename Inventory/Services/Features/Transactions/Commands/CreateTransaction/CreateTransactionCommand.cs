using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(CreateTransactionRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => Array.Empty<string>();
    public string[] CacheKeyPrefixes => new[] { "Transactions-" };
}
