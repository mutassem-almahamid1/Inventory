using Services.Abstractions.Persistence;
using FluentValidation;

namespace Services.Features.Categories.Commands.UpdateCategory;


public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(IUnitOfWork unitOfWork)
    {
        // Category ID is always required
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) =>
            {
                return await unitOfWork.Categories.ExistsAsync(id, ct);
            }).WithMessage("Category with this ID does not exist.");

        RuleFor(c => c.Request.Name)
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(async (cmd, name, ct) =>
            {
                if (string.IsNullOrWhiteSpace(name)) return true; // Skip if not provided
                var existing = await unitOfWork.Categories.GetByNameAsync(name);
                return existing == null || existing.Id == cmd.Id;
            }).WithMessage("Another category with this name already exists.")
            .When(c => !string.IsNullOrWhiteSpace(c.Request.Name));

        RuleFor(c => c.Request.Description)
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.")
            .When(c => c.Request.Description != null);

        RuleFor(c => c.Request)
            .Must(r => 
                !string.IsNullOrWhiteSpace(r.Name) ||
                r.Description != null)
            .WithMessage("At least one field must be provided for update.");
    }
}
