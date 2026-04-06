using Core.Common;
using MediatR;

namespace Services.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : IRequest<Result<Guid>>;
