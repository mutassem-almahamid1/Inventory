using Services.Abstractions.Persistence;
using FluentValidation;

namespace Services.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(p => p.Request.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(async (name, ct) =>
            {
                var exists = await unitOfWork.Products.GetByNameAsync(name);
                return exists == null;
            }).WithMessage("Product with this name already exists.");

        RuleFor(p => p.Request.Description)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(p => p.Request.UnitPrice)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        
        RuleFor(p => p.Request.CategoryId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (categoryId, ct) =>
            {
                return await unitOfWork.Categories.ExistsAsync(categoryId, ct);
            }).WithMessage("Category with this ID does not exist.");

        RuleFor(p => p.Request.InventoryId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (inventoryId, ct) =>
            {
                return await unitOfWork.Inventories.ExistsAsync(inventoryId, ct);
            }).WithMessage("Inventory with this ID does not exist.");

        RuleFor(p => p.Request.TransactionId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (transactionId, ct) =>
            {
                return await unitOfWork.Transactions.ExistsAsync(transactionId, ct);
            }).WithMessage("Transaction with this ID does not exist.");
    }
}
