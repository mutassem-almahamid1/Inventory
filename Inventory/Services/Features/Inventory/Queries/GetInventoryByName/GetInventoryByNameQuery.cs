using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetInventoryByName;

public record GetInventoryByNameQuery(string Name) : IRequest<Result<InventoryResponse>>;
