using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(Guid Id) : IRequest<Result<TransactionResponse>>;
