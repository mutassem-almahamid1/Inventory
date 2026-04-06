using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Orders.ExistsAsync(id, ct))
            .WithMessage("Order with this ID does not exist.");

        RuleFor(x => x.Request.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.")
            .When(x => x.Request.TotalAmount.HasValue);

        RuleFor(x => x.Request.Status)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.")
            .When(x => x.Request.Status.HasValue);

        RuleFor(x => x.Request.OrderType)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.")
            .When(x => x.Request.OrderType.HasValue);

        RuleFor(x => x.Request.ExpectedDeliveryDate)
            .Must((cmd, expectedDate) =>
            {
                if (!expectedDate.HasValue) return true;

                var orderDate = cmd.Request.OrderDate;
                return !orderDate.HasValue || expectedDate.Value >= orderDate.Value;
            })
            .WithMessage("ExpectedDeliveryDate must be greater than or equal to OrderDate.")
            .When(x => x.Request.ExpectedDeliveryDate.HasValue);

        RuleFor(x => x.Request.ReceivedDate)
            .Must((cmd, receivedDate) =>
            {
                if (!receivedDate.HasValue) return true;

                var orderDate = cmd.Request.OrderDate;
                return !orderDate.HasValue || receivedDate.Value >= orderDate.Value;
            })
            .WithMessage("ReceivedDate must be greater than or equal to OrderDate.")
            .When(x => x.Request.ReceivedDate.HasValue);

        RuleFor(x => x.Request)
            .Must(r =>
                r.SupplierId.HasValue ||
                r.OrderDate.HasValue ||
                r.TotalAmount.HasValue ||
                r.Status.HasValue ||
                r.ExpectedDeliveryDate.HasValue ||
                r.OrderType.HasValue ||
                r.ReceivedDate.HasValue)
            .WithMessage("At least one field must be provided for update.");
    }
}
