using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByPriceRange;

public record GetProductsByPriceRangeQuery(decimal MinPrice, decimal MaxPrice) : IRequest<Result<List<ProductResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Products-PriceRange-{MinPrice}-{MaxPrice}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
