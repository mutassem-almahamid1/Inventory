using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.OrderDetails.Commands.CreateOrderDetail;

public record CreateOrderDetailCommand(CreateOrderDetailRequest Request) : IRequest<Result<Guid>>;
