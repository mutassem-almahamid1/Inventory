using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.OrderDetails.Commands.CreateOrderDetail;

public class CreateOrderDetailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderDetailCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = OrderDetailMapper.ToEntity(request.Request);

        await unitOfWork.OrderDetails.AddAsync(orderDetail);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(orderDetail.Id);
    }
}
