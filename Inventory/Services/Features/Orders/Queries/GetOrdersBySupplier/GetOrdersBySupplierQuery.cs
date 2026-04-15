using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Orders.Queries.GetOrdersBySupplier;

public record GetOrdersBySupplierQuery(Guid SupplierId) : IRequest<Result<List<OrderResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Orders-Supplier-{SupplierId}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
