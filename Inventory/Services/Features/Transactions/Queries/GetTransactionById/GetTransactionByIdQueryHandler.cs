using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTransactionByIdQuery, Result<TransactionResponse>>
{
    public async Task<Result<TransactionResponse>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.Transactions.GetByIdAsync(request.Id, cancellationToken);
        if (transaction is null)
            return Result.Failure<TransactionResponse>($"Transaction with ID {request.Id} not found.");

        return Result.Success(TransactionMapper.ToResponse(transaction));
    }
}
