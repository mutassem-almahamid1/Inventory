using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailsByOrderId;

public class GetOrderDetailsByOrderIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrderDetailsByOrderIdQuery, Result<List<OrderDetailResponse>>>
{
    public async Task<Result<List<OrderDetailResponse>>> Handle(GetOrderDetailsByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await unitOfWork.OrderDetails.GetByOrderIdAsync(request.OrderId, cancellationToken);
        var response = orderDetails.Select(OrderDetailMapper.ToResponse).ToList();

        return Result.Success(response);
    }
}
