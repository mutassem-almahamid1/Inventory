using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetInventoryByName;

public record GetInventoryByNameQuery(string Name) : IRequest<Result<InventoryResponse>>, ICacheableQuery
{
    public string CacheKey => $"Inventory-Name-{Name}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
