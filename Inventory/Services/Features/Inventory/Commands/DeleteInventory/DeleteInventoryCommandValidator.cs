using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Inventory.Commands.DeleteInventory;

public class DeleteInventoryCommandValidator : AbstractValidator<DeleteInventoryCommand>
{
    public DeleteInventoryCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Inventories.ExistsAsync(id, ct))
            .WithMessage("Inventory with this ID does not exist.");
    }
}
