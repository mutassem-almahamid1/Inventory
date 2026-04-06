using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Inventory.Commands.CreateInventory;

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(i => i.Request.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(async (name, ct) =>
            {
                var existing = await unitOfWork.Inventories.GetByNameAsync(name, ct);
                return existing == null;
            }).WithMessage("Inventory with this name already exists.");

        RuleFor(i => i.Request.Location)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

        RuleFor(i => i.Request.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.");

        RuleFor(i => i.Request.ReorderLevel)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.");
    }
}
