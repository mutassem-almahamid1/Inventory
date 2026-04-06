using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetTransactionsByEmployee;

public record GetTransactionsByEmployeeQuery(Guid EmployeeId) : IRequest<Result<List<TransactionResponse>>>;
