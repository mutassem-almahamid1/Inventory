using FluentValidation;

namespace Services.Features.Orders.Queries.GetOrdersBySupplier;

public class GetOrdersBySupplierQueryValidator : AbstractValidator<GetOrdersBySupplierQuery>
{
    public GetOrdersBySupplierQueryValidator()
    {
        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
