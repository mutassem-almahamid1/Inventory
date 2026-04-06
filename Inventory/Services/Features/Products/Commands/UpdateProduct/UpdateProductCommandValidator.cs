using Services.Abstractions.Persistence;
using FluentValidation;

namespace Services.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IUnitOfWork unitOfWork)
    {
        // Product ID is always required
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) =>
            {
                return await unitOfWork.Products.ExistsAsync(id, ct);
            }).WithMessage("Product with this ID does not exist.");

        RuleFor(p => p.Request.Name)
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
            .MustAsync(async (cmd, name, ct) =>
            {
                if (string.IsNullOrWhiteSpace(name)) return true; // Skip if not provided
                var existing = await unitOfWork.Products.GetByNameAsync(name);
                return existing == null || existing.Id == cmd.Id;
            }).WithMessage("Another product with this name already exists.")
            .When(p => !string.IsNullOrWhiteSpace(p.Request.Name));

        RuleFor(p => p.Request.Description)
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.")
            .When(p => p.Request.Description != null);

        RuleFor(p => p.Request.UnitPrice)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
            .When(p => p.Request.UnitPrice.HasValue);

        RuleFor(p => p.Request.CategoryId)
            .MustAsync(async (categoryId, ct) =>
            {
                if (!categoryId.HasValue) return true;
                return await unitOfWork.Categories.ExistsAsync(categoryId.Value, ct);
            }).WithMessage("Category with this ID does not exist.")
            .When(p => p.Request.CategoryId.HasValue);

        RuleFor(p => p.Request.InventoryId)
            .MustAsync(async (inventoryId, ct) =>
            {
                if (!inventoryId.HasValue) return true;
                return await unitOfWork.Inventories.ExistsAsync(inventoryId.Value, ct);
            }).WithMessage("Inventory with this ID does not exist.")
            .When(p => p.Request.InventoryId.HasValue);

        RuleFor(p => p.Request.TransactionId)
            .MustAsync(async (transactionId, ct) =>
            {
                if (!transactionId.HasValue) return true;
                return await unitOfWork.Transactions.ExistsAsync(transactionId.Value, ct);
            }).WithMessage("Transaction with this ID does not exist.")
            .When(p => p.Request.TransactionId.HasValue);

        RuleFor(p => p.Request)
            .Must(r => 
                !string.IsNullOrWhiteSpace(r.Name) ||
                r.Description != null ||
                r.UnitPrice.HasValue ||
                r.Weight.HasValue ||
                r.Length.HasValue ||
                r.Width.HasValue ||
                r.Height.HasValue ||
                r.TaxCost.HasValue ||
                r.ProfitPerUnit.HasValue ||
                r.ProductionCost.HasValue ||
                r.CategoryId.HasValue ||
                r.InventoryId.HasValue ||
                r.TransactionId.HasValue)
            .WithMessage("At least one field must be provided for update.");
    }
}
