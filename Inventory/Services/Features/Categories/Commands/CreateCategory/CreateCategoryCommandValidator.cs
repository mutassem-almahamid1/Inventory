using Services.Abstractions.Persistence;
using FluentValidation;

namespace Services.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.Request.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(async (name, ct) =>
            {
                var exists = await unitOfWork.Categories.GetByNameAsync(name);
                return exists == null;
            }).WithMessage("Category with this name already exists.");

        RuleFor(c => c.Request.Description)
            .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
    }
}
