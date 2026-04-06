using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.OrderDetails.Commands.UpdateOrderDetail;

public class UpdateOrderDetailCommandValidator : AbstractValidator<UpdateOrderDetailCommand>
{
    public UpdateOrderDetailCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.OrderDetails.ExistsAsync(id, ct))
            .WithMessage("OrderDetail with this ID does not exist.");

        RuleFor(x => x.Request.Quantity)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
            .When(x => x.Request.Quantity.HasValue);

        RuleFor(x => x.Request.TotalPrice)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.")
            .When(x => x.Request.TotalPrice.HasValue);

        RuleFor(x => x.Request.OrderId)
            .MustAsync(async (orderId, ct) =>
            {
                if (!orderId.HasValue) return true;
                return await unitOfWork.Orders.ExistsAsync(orderId.Value, ct);
            })
            .WithMessage("Order with this ID does not exist.")
            .When(x => x.Request.OrderId.HasValue);

        RuleFor(x => x.Request.ProductId)
            .MustAsync(async (productId, ct) =>
            {
                if (!productId.HasValue) return true;
                return await unitOfWork.Products.ExistsAsync(productId.Value, ct);
            })
            .WithMessage("Product with this ID does not exist.")
            .When(x => x.Request.ProductId.HasValue);

        RuleFor(x => x.Request)
            .Must(r =>
                r.Quantity.HasValue ||
                r.TotalPrice.HasValue ||
                r.OrderId.HasValue ||
                r.ProductId.HasValue)
            .WithMessage("At least one field must be provided for update.");
    }
}
