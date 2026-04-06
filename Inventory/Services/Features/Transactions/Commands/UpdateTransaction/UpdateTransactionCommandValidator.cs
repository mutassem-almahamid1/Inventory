using FluentValidation;
using Services.Abstractions.Persistence;

namespace Services.Features.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
{
    public UpdateTransactionCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, ct) => await unitOfWork.Transactions.ExistsAsync(id, ct))
            .WithMessage("Transaction with this ID does not exist.");

        RuleFor(x => x.Request.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .When(x => x.Request.EmployeeId.HasValue);

        RuleFor(x => x.Request.TransactionDate)
            .NotEqual(default(DateTimeOffset)).WithMessage("{PropertyName} is required.")
            .When(x => x.Request.TransactionDate.HasValue);

        RuleFor(x => x.Request.Quantity)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
            .When(x => x.Request.Quantity.HasValue);

        RuleFor(x => x.Request.TransactionType)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.")
            .When(x => x.Request.TransactionType.HasValue);

        RuleFor(x => x.Request.Notes)
            .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.")
            .When(x => x.Request.Notes is not null);

        RuleFor(x => x.Request)
            .Must(r =>
                r.EmployeeId.HasValue ||
                r.TransactionDate.HasValue ||
                r.Quantity.HasValue ||
                r.TransactionType.HasValue ||
                r.Notes is not null)
            .WithMessage("At least one field must be provided for update.");
    }
}
