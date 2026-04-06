using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var existingInventory = await _unitOfWork.Inventories.GetByIdAsync(request.Id, cancellationToken);
        if (existingInventory == null)
            return Result.Failure<Guid>($"Inventory with ID {request.Id} not found.");

        InventoryMapper.ToEntity(request.Request, existingInventory);

        _unitOfWork.Inventories.Update(existingInventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingInventory.Id);
    }
}
