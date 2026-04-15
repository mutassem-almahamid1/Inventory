using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<Result<List<ProductResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Products-Category-{CategoryId}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}

