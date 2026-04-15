using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByInventory;

public record GetProductsByInventoryQuery(Guid Id) : IRequest<Result<List<ProductResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Products-Inventory-{Id}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
