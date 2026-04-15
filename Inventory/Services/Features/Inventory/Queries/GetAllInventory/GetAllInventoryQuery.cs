using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetAllInventory;

public record GetAllInventoryQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<InventoryResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Inventory-All-{PageNumber}-{PageSize}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}
