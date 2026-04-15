using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductByName;

public record GetProductByNameQuery(string Name) : IRequest<Result<ProductResponse>>, ICacheableQuery
{
    public string CacheKey => $"Product-Name-{Name}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
