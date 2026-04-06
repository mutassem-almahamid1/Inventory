using FluentValidation;

namespace Services.Features.Products.Queries.GetTopProfitableProducts;

public class GetTopProfitableProductsQueryValidator : AbstractValidator<GetTopProfitableProductsQuery>
{
    public GetTopProfitableProductsQueryValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .WithMessage("Count must be greater than 0.");
    }
}
