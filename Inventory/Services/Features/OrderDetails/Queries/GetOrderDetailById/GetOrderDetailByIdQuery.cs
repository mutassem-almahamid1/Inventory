using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.OrderDetails.Queries.GetOrderDetailById;

public record GetOrderDetailByIdQuery(Guid Id) : IRequest<Result<OrderDetailResponse>>, ICacheableQuery
{
    public string CacheKey => $"OrderDetail-{Id}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
