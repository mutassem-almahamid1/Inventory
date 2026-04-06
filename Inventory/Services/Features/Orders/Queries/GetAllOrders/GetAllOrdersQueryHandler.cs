using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrdersQuery, Result<List<OrderResponse>>>
{
    public async Task<Result<List<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork.Orders.GetAllAsync(cancellationToken);
        var response = orders.Select(OrderMapper.ToResponse).ToList();

        return Result.Success(response);
    }
}
