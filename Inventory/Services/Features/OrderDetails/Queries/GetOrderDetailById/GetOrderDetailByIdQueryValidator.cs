using FluentValidation;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailById;

public class GetOrderDetailByIdQueryValidator : AbstractValidator<GetOrderDetailByIdQuery>
{
    public GetOrderDetailByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
