using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid Id, UpdateOrderRequest Request) : IRequest<Result<Guid>>;
