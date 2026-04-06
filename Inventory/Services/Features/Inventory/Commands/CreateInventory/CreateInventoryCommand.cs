using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Inventory.Commands.CreateInventory;

public record CreateInventoryCommand(CreateInventoryRequest Request) : IRequest<Result<Guid>>;
