using Core.Common;
using MediatR;

namespace Services.Features.Inventory.Commands.DeleteInventory;

public record DeleteInventoryCommand(Guid Id) : IRequest<Result<Guid>>;
