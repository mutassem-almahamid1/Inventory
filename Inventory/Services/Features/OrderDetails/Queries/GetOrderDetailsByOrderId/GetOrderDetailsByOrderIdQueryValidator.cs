using FluentValidation;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailsByOrderId;

public class GetOrderDetailsByOrderIdQueryValidator : AbstractValidator<GetOrderDetailsByOrderIdQuery>
{
    public GetOrderDetailsByOrderIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
