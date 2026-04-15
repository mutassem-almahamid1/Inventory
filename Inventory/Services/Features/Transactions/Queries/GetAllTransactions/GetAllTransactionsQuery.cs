using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetAllTransactions;

public record GetAllTransactionsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<TransactionResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Transactions-All-{PageNumber}-{PageSize}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
