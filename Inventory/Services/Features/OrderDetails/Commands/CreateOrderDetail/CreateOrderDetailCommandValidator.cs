using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.OrderDetails.Commands.CreateOrderDetail;

public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
{
    public CreateOrderDetailCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Request.Quantity)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.Request.TotalPrice)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

        RuleFor(x => x.Request.OrderId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (orderId, ct) => await unitOfWork.Orders.ExistsAsync(orderId, ct))
            .WithMessage("Order with this ID does not exist.");

        RuleFor(x => x.Request.ProductId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (productId, ct) => await unitOfWork.Products.ExistsAsync(productId, ct))
            .WithMessage("Product with this ID does not exist.");
    }
}
