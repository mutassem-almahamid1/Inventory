using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;

namespace Services.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTransactionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var existingTransaction = await unitOfWork.Transactions.GetByIdAsync(request.Id, cancellationToken);
        if (existingTransaction is null)
            return Result.Failure<Guid>($"Transaction with ID {request.Id} not found.");

        unitOfWork.Transactions.Delete(existingTransaction);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingTransaction.Id);
    }
}
