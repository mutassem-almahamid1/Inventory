using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderResponse>>, ICacheableQuery
{
    public string CacheKey => $"Order-{Id}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
