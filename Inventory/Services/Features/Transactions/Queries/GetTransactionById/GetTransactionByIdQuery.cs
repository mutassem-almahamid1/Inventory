using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(Guid Id) : IRequest<Result<TransactionResponse>>, ICacheableQuery
{
    public string CacheKey => $"Transaction-{Id}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
