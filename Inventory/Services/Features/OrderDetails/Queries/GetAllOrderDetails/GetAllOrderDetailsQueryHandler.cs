using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetAllOrderDetails;

public class GetAllOrderDetailsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllOrderDetailsQuery, Result<List<OrderDetailResponse>>>
{
    public async Task<Result<List<OrderDetailResponse>>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await unitOfWork.OrderDetails.GetAllAsync(cancellationToken);
        var response = orderDetails.Select(OrderDetailMapper.ToResponse).ToList();

        return Result.Success(response);
    }
}
