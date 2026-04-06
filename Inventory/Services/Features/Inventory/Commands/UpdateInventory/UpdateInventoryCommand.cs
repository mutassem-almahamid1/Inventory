using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Inventory.Commands.UpdateInventory;

public record UpdateInventoryCommand(Guid Id, UpdateInventoryRequest Request) : IRequest<Result<Guid>>;
