using Core.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Services.Mappings;

public static class InventoryMapper
{
    public static Inventory ToEntity(CreateInventoryRequest request)
    {
        return new Inventory
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Quantity = request.Quantity,
            ReorderLevel = request.ReorderLevel,
            Location = request.Location,
            CreatedBy = Guid.NewGuid(),
            CreatedOn = DateTimeOffset.UtcNow,
            ModifiedOn = DateTimeOffset.UtcNow
        };
    }

    public static Inventory ToEntity(UpdateInventoryRequest request, Inventory inventory)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            inventory.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Location))
            inventory.Location = request.Location;

        if (request.Quantity.HasValue)
            inventory.Quantity = request.Quantity.Value;

        if (request.ReorderLevel.HasValue)
            inventory.ReorderLevel = request.ReorderLevel.Value;

        inventory.ModifiedOn = DateTimeOffset.UtcNow;
        inventory.ModifiedBy = Guid.NewGuid();

        return inventory;
    }

    public static InventoryResponse ToResponse(Inventory inventory)
    {
        return new InventoryResponse
        {
            Id = inventory.Id,
            Name = inventory.Name,
            Quantity = inventory.Quantity,
            ReorderLevel = inventory.ReorderLevel,
            Location = inventory.Location,
        };
    }
}
