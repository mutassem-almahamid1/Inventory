using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailById;

public class GetOrderDetailByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrderDetailByIdQuery, Result<OrderDetailResponse>>
{
    public async Task<Result<OrderDetailResponse>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var orderDetail = await unitOfWork.OrderDetails.GetByIdAsync(request.Id, cancellationToken);
        if (orderDetail is null)
            return Result.Failure<OrderDetailResponse>($"OrderDetail with ID {request.Id} not found.");

        return Result.Success(OrderDetailMapper.ToResponse(orderDetail));
    }
}
