using Core.Common;
using Core.Interfaces;
using MediatR;

namespace Services.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Order-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Orders-" };
}
