using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.OrderDetails.Commands.UpdateOrderDetail;

public record UpdateOrderDetailCommand(Guid Id, UpdateOrderDetailRequest Request) : IRequest<Result<Guid>>;
