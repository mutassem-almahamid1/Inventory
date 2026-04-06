using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Inventory.Commands.UpdateInventoryProductQuantity;

public class UpdateInventoryProductQuantityCommandValidator : AbstractValidator<UpdateInventoryProductQuantityCommand>
{
    public UpdateInventoryProductQuantityCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.InventoryId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Inventories.ExistsAsync(id, ct))
            .WithMessage("Inventory with this ID does not exist.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Products.ExistsAsync(id, ct))
            .WithMessage("Product with this ID does not exist.");

        RuleFor(x => x.Request.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.");
    }
}

