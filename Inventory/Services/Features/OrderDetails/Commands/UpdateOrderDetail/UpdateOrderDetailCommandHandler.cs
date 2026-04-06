using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.OrderDetails.Commands.UpdateOrderDetail;

public class UpdateOrderDetailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderDetailCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var existingOrderDetail = await unitOfWork.OrderDetails.GetByIdAsync(request.Id, cancellationToken);
        if (existingOrderDetail is null)
            return Result.Failure<Guid>($"OrderDetail with ID {request.Id} not found.");

        OrderDetailMapper.ToEntity(request.Request, existingOrderDetail);
        unitOfWork.OrderDetails.Update(existingOrderDetail);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingOrderDetail.Id);
    }
}
