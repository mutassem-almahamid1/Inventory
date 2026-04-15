using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailsByOrderId;

public record GetOrderDetailsByOrderIdQuery(Guid OrderId) : IRequest<Result<List<OrderDetailResponse>>>, ICacheableQuery
{
    public string CacheKey => $"OrderDetails-Order-{OrderId}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
