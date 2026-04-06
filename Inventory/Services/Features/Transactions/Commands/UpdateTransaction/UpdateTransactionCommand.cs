using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(Guid Id, UpdateTransactionRequest Request) : IRequest<Result<Guid>>;
