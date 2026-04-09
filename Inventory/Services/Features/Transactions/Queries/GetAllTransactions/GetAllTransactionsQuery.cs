using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetAllTransactions;

public record GetAllTransactionsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<TransactionResponse>>>;
