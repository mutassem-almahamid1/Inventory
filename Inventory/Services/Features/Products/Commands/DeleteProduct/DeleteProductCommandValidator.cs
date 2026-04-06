using Services.Abstractions.Persistence;
using FluentValidation;

namespace Services.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) =>
            {
                return await unitOfWork.Products.ExistsAsync(id, ct);
            }).WithMessage("Product with this ID does not exist.");
    }
}
