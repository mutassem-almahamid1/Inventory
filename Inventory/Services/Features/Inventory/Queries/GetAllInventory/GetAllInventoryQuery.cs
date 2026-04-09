using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetAllInventory;

public record GetAllInventoryQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<InventoryResponse>>>;
