using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(CreateTransactionRequest Request) : IRequest<Result<Guid>>;
