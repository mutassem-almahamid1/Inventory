using Core.Common;
using Core.Interfaces;
using MediatR;

namespace Services.Features.Inventory.Commands.DeleteInventory;

public record DeleteInventoryCommand(Guid Id) : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public string[] CacheKeys => new[] { $"Inventory-{Id}" };
    public string[] CacheKeyPrefixes => new[] { "Inventory-" };
}
