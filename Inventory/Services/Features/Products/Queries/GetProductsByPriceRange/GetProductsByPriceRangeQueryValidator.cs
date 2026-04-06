using FluentValidation;

namespace Services.Features.Products.Queries.GetProductsByPriceRange;

public class GetProductsByPriceRangeQueryValidator : AbstractValidator<GetProductsByPriceRangeQuery>
{
    public GetProductsByPriceRangeQueryValidator()
    {
        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Minimum price must be greater than or equal to 0.");
        
        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Maximum price must be greater than or equal to 0.");
        
        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(x => x.MinPrice)
            .WithMessage("Maximum price must be greater than or equal to minimum price.");
    }
}
