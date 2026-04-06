using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;

namespace Services.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await unitOfWork.Orders.GetByIdAsync(request.Id, cancellationToken);
        if (existingOrder is null)
            return Result.Failure<Guid>($"Order with ID {request.Id} not found.");

        unitOfWork.Orders.Delete(existingOrder);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingOrder.Id);
    }
}
