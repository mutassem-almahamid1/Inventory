using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;

namespace Services.Features.Inventory.Commands.DeleteInventory;

public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _unitOfWork.Inventories.GetByIdAsync(request.Id, cancellationToken);
        if (inventory == null)
            return Result.Failure<Guid>($"Inventory with ID {request.Id} not found.");

        _unitOfWork.Inventories.Delete(inventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(inventory.Id);
    }
}
