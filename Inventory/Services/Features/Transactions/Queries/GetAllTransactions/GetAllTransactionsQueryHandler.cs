using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Transactions.Queries.GetAllTransactions;

public class GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllTransactionsQuery, Result<PagedResponse<TransactionResponse>>>
{
    public async Task<Result<PagedResponse<TransactionResponse>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var pagedResponse = await unitOfWork.Transactions.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var response = pagedResponse.Data.Select(TransactionMapper.ToResponse).ToList();

        var result = new PagedResponse<TransactionResponse>(
            response,
            pagedResponse.TotalCount,
            pagedResponse.PageNumber,
            pagedResponse.PageSize
        );

        return Result.Success(result);
    }
}
