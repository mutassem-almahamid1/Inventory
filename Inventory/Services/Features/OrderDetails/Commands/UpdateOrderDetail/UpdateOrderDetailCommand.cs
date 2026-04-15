using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.OrderDetails.Commands.UpdateOrderDetail;

public record UpdateOrderDetailCommand(Guid Id, UpdateOrderDetailRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"OrderDetail-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "OrderDetails-" };
}
