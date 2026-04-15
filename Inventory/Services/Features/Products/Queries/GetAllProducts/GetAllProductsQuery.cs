using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<ProductResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Products-All-{PageNumber}-{PageSize}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
