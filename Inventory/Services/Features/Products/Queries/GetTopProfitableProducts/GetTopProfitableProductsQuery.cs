using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetTopProfitableProducts;

public record GetTopProfitableProductsQuery(int Count) : IRequest<Result<List<ProductResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Products-TopProfitable-{Count}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
