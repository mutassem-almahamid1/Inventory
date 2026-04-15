using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid Id, UpdateOrderRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Order-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Orders-" };
}
