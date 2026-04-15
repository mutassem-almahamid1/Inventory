using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetAllOrderDetails;

public record GetAllOrderDetailsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<OrderDetailResponse>>>, ICacheableQuery
{
    public string CacheKey => $"OrderDetails-All-{PageNumber}-{PageSize}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
