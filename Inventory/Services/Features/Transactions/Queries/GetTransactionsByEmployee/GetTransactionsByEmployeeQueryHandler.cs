using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetTransactionsByEmployee;

public class GetTransactionsByEmployeeQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTransactionsByEmployeeQuery, Result<List<TransactionResponse>>>
{
    public async Task<Result<List<TransactionResponse>>> Handle(GetTransactionsByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.Transactions.GetByEmployeeIdAsync(request.EmployeeId, cancellationToken);
        var response = transactions.Select(TransactionMapper.ToResponse).ToList();

        return Result.Success(response);
    }
}
