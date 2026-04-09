using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetAllOrderDetails;

public class GetAllOrderDetailsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllOrderDetailsQuery, Result<PagedResponse<OrderDetailResponse>>>
{
    public async Task<Result<PagedResponse<OrderDetailResponse>>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var pagedResponse = await unitOfWork.OrderDetails.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var response = pagedResponse.Data.Select(OrderDetailMapper.ToResponse).ToList();

        var result = new PagedResponse<OrderDetailResponse>(
            response,
            pagedResponse.TotalCount,
            pagedResponse.PageNumber,
            pagedResponse.PageSize
        );

        return Result.Success(result);
    }
}
