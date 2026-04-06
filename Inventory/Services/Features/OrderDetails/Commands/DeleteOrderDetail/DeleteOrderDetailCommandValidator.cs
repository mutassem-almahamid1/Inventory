using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.OrderDetails.Commands.DeleteOrderDetail;

public class DeleteOrderDetailCommandValidator : AbstractValidator<DeleteOrderDetailCommand>
{
    public DeleteOrderDetailCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.OrderDetails.ExistsAsync(id, ct))
            .WithMessage("OrderDetail with this ID does not exist.");
    }
}
