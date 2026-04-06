using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await unitOfWork.Orders.GetByIdAsync(request.Id, cancellationToken);
        if (existingOrder is null)
            return Result.Failure<Guid>($"Order with ID {request.Id} not found.");

        OrderMapper.ToEntity(request.Request, existingOrder);
        unitOfWork.Orders.Update(existingOrder);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingOrder.Id);
    }
}
