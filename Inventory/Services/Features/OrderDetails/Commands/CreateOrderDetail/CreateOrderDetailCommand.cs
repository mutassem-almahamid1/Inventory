using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.OrderDetails.Commands.CreateOrderDetail;

public record CreateOrderDetailCommand(CreateOrderDetailRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => Array.Empty<string>();
    public string[] CacheKeyPrefixes => new[] { "OrderDetails-" };
}
