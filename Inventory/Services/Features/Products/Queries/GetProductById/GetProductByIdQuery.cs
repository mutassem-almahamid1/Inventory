using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductResponse>>, ICacheableQuery
{
    public string CacheKey => $"Product-{Id}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
