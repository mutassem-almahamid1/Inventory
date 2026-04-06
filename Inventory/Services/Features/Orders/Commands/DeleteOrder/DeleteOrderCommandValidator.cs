using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Orders.ExistsAsync(id, ct))
            .WithMessage("Order with this ID does not exist.");
    }
}
