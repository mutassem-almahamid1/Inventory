using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
{
    public DeleteTransactionCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Transactions.ExistsAsync(id, ct))
            .WithMessage("Transaction with this ID does not exist.");
    }
}
