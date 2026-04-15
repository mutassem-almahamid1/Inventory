using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetLowStockInventory;

public record GetLowStockInventoryQuery : IRequest<Result<List<InventoryResponse>>>, ICacheableQuery
{
    public string CacheKey => "Inventory-LowStock";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}


