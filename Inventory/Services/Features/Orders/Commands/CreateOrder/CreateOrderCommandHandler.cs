using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = OrderMapper.ToEntity(request.Request);

        await unitOfWork.Orders.AddAsync(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(order.Id);
    }
}
