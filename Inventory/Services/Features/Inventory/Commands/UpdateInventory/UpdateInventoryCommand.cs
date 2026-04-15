using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Inventory.Commands.UpdateInventory;

public record UpdateInventoryCommand(Guid Id, UpdateInventoryRequest Request) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Inventory-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Inventory-" };
}
