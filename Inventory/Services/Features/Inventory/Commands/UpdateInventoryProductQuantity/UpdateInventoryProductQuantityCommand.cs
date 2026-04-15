using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Requests;

namespace Services.Features.Inventory.Commands.UpdateInventoryProductQuantity;

public record UpdateInventoryProductQuantityCommand : IRequest<Result<Guid>>, ICacheInvalidatorCommand
{
    public Guid InventoryId { get; }
    public Guid ProductId { get; }
    public UpdateInventoryProductQuantityRequest Request { get; }

    public string[] CacheKeys => new[] { $"Inventory-{InventoryId}" };
    public string[] CacheKeyPrefixes => new[] { "Inventory-" };

    public UpdateInventoryProductQuantityCommand(Guid inventoryId, Guid productId, UpdateInventoryProductQuantityRequest request)
    {
        InventoryId = inventoryId;
        ProductId = productId;
        Request = request;
    }
}


