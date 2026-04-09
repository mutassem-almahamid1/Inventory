using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrdersQuery, Result<PagedResponse<OrderResponse>>>
{
    public async Task<Result<PagedResponse<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var pagedResponse = await unitOfWork.Orders.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var response = pagedResponse.Data.Select(OrderMapper.ToResponse).ToList();

        var result = new PagedResponse<OrderResponse>(
            response,
            pagedResponse.TotalCount,
            pagedResponse.PageNumber,
            pagedResponse.PageSize
        );

        return Result.Success(result);
    }
}
