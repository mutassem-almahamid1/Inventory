using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetTransactionsByEmployee;

public record GetTransactionsByEmployeeQuery(Guid EmployeeId) : IRequest<Result<List<TransactionResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Transactions-Employee-{EmployeeId}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
