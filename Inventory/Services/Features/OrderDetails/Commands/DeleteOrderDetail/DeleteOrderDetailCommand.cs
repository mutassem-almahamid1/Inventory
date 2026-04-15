using Core.Common;
using Core.Interfaces;
using MediatR;

namespace Services.Features.OrderDetails.Commands.DeleteOrderDetail;

public record DeleteOrderDetailCommand(Guid Id) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"OrderDetail-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "OrderDetails-" };
}
