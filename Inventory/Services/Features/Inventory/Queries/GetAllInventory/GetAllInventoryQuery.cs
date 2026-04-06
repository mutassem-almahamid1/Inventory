using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetAllInventory;

public record GetAllInventoryQuery : IRequest<Result<List<InventoryResponse>>>;
