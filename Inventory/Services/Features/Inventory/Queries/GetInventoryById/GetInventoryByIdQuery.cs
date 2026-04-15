using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetInventoryById;

public record GetInventoryByIdQuery(Guid Id) : IRequest<Result<InventoryResponse>>, ICacheableQuery
{
    public string CacheKey => $"Inventory-{Id}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}
