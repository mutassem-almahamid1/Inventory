using Core.Common;
using MediatR;

namespace Services.Features.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(Guid Id) : IRequest<Result<Guid>>;
