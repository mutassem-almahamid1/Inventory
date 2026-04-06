using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetInventoryById;

public record GetInventoryByIdQuery(Guid Id) : IRequest<Result<InventoryResponse>>;
