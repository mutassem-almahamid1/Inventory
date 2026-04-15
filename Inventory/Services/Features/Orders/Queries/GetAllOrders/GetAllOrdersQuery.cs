using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<OrderResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Orders-All-{PageNumber}-{PageSize}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
