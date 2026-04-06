using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderByIdQuery, Result<OrderResponse>>
{
    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(request.Id, cancellationToken);
        if (order is null)
            return Result.Failure<OrderResponse>($"Order with ID {request.Id} not found.");

        return Result.Success(OrderMapper.ToResponse(order));
    }
}
