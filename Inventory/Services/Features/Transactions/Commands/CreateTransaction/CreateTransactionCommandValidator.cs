using FluentValidation;

namespace Services.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.Request.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Request.TransactionDate)
            .NotEqual(default(DateTimeOffset)).WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Request.Quantity)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(x => x.Request.TransactionType)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

        RuleFor(x => x.Request.Notes)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");
    }
}
