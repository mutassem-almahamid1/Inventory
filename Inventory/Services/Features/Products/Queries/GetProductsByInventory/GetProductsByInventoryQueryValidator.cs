using FluentValidation;

namespace Services.Features.Products.Queries.GetProductsByInventory;

public class GetProductsByInventoryQueryValidator : AbstractValidator<GetProductsByInventoryQuery>
{
    public GetProductsByInventoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
