using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetAllTransactions;

public record GetAllTransactionsQuery : IRequest<Result<List<TransactionResponse>>>;
