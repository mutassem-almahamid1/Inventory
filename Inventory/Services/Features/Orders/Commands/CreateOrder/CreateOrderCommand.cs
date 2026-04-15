using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(CreateOrderRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => Array.Empty<string>();
    public string[] CacheKeyPrefixes => new[] { "Orders-" };
}
