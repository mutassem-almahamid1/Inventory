using FluentValidation;

namespace Services.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Request.SupplierId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Request.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

        RuleFor(x => x.Request.Status)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

        RuleFor(x => x.Request.OrderType)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

        RuleFor(x => x.Request.ExpectedDeliveryDate)
            .GreaterThanOrEqualTo(x => x.Request.OrderDate)
            .WithMessage("ExpectedDeliveryDate must be greater than or equal to OrderDate.");

        RuleFor(x => x.Request.ReceivedDate)
            .GreaterThanOrEqualTo(x => x.Request.OrderDate)
            .WithMessage("ReceivedDate must be greater than or equal to OrderDate.");
    }
}
