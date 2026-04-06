using Core.Common;
using MediatR;

namespace Services.Features.OrderDetails.Commands.DeleteOrderDetail;

public record DeleteOrderDetailCommand(Guid Id) : IRequest<Result<Guid>>;
