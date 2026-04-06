using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;

namespace Services.Features.Inventory.Commands.UpdateInventoryProductQuantity;

public class UpdateInventoryProductQuantityCommandHandler : IRequestHandler<UpdateInventoryProductQuantityCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInventoryProductQuantityCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateInventoryProductQuantityCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _unitOfWork.Inventories.GetByIdAsync(request.InventoryId, cancellationToken);
        if (inventory == null)
            return Result.Failure<Guid>($"Inventory with ID {request.InventoryId} not found.");

        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
            return Result.Failure<Guid>($"Product with ID {request.ProductId} not found.");

        if (product.InventoryId != inventory.Id)
            return Result.Failure<Guid>($"Product with ID {request.ProductId} does not belong to Inventory with ID {request.InventoryId}.");

        inventory.Quantity = request.Request.Quantity;
        inventory.ModifiedOn = DateTimeOffset.UtcNow;
        inventory.ModifiedBy = Guid.NewGuid();

        _unitOfWork.Inventories.Update(inventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(inventory.Id);
    }
}

