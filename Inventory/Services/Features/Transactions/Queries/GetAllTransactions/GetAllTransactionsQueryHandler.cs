using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetAllTransactions;

public class GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllTransactionsQuery, Result<List<TransactionResponse>>>
{
    public async Task<Result<List<TransactionResponse>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.Transactions.GetAllAsync(cancellationToken);
        var response = transactions.Select(TransactionMapper.ToResponse).ToList();

        return Result.Success(response);
    }
}
