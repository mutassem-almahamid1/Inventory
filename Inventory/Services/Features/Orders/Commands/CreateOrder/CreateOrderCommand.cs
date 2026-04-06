using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(CreateOrderRequest Request) : IRequest<Result<Guid>>;
