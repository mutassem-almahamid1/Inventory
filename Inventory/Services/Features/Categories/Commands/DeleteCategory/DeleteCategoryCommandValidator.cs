using Services.Abstractions.Persistence;
using FluentValidation;

namespace Services.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) =>
            {
                return await unitOfWork.Categories.ExistsAsync(id, ct);
            }).WithMessage("Category with this ID does not exist.");
    }
}
