using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;

namespace Services.Features.OrderDetails.Commands.DeleteOrderDetail;

public class DeleteOrderDetailCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteOrderDetailCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var existingOrderDetail = await unitOfWork.OrderDetails.GetByIdAsync(request.Id, cancellationToken);
        if (existingOrderDetail is null)
            return Result.Failure<Guid>($"OrderDetail with ID {request.Id} not found.");

        unitOfWork.OrderDetails.Delete(existingOrderDetail);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingOrderDetail.Id);
    }
}
