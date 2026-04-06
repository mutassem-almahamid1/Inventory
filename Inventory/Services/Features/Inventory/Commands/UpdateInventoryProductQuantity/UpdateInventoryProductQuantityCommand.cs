using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Inventory.Commands.UpdateInventoryProductQuantity;

public record UpdateInventoryProductQuantityCommand : IRequest<Result<Guid>>
{
	public Guid InventoryId { get; }
	public Guid ProductId { get; }
	public UpdateInventoryProductQuantityRequest Request { get; }

	public UpdateInventoryProductQuantityCommand(Guid inventoryId, Guid productId, UpdateInventoryProductQuantityRequest request)
	{
		InventoryId = inventoryId;
		ProductId = productId;
		Request = request;
	}
}


