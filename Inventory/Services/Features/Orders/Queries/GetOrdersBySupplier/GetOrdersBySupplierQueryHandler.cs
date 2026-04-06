using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetOrdersBySupplier;

public class GetOrdersBySupplierQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrdersBySupplierQuery, Result<List<OrderResponse>>>
{
    public async Task<Result<List<OrderResponse>>> Handle(GetOrdersBySupplierQuery request, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork.Orders.GetBySupplierIdAsync(request.SupplierId, cancellationToken);
        var response = orders.Select(OrderMapper.ToResponse).ToList();

        return Result.Success(response);
    }
}
