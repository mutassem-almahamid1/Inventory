using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetLowStockInventory;

public record GetLowStockInventoryQuery : IRequest<Result<List<InventoryResponse>>>;


