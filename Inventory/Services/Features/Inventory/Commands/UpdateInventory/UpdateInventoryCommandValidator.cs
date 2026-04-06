using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Inventories.ExistsAsync(id, ct))
            .WithMessage("Inventory with this ID does not exist.");

        RuleFor(i => i.Request.Name)
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(async (cmd, name, ct) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                    return true;

                var existing = await unitOfWork.Inventories.GetByNameAsync(name, ct);
                return existing == null || existing.Id == cmd.Id;
            }).WithMessage("Another inventory with this name already exists.")
            .When(i => !string.IsNullOrWhiteSpace(i.Request.Name));

        RuleFor(i => i.Request.Location)
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.")
            .When(i => i.Request.Location != null);

        RuleFor(i => i.Request.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.")
            .When(i => i.Request.Quantity.HasValue);

        RuleFor(i => i.Request.ReorderLevel)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.")
            .When(i => i.Request.ReorderLevel.HasValue);

        RuleFor(i => i.Request)
            .Must(r =>
                !string.IsNullOrWhiteSpace(r.Name) ||
                r.Location != null ||
                r.Quantity.HasValue ||
                r.ReorderLevel.HasValue)
            .WithMessage("At least one field must be provided for update.");
    }
}
