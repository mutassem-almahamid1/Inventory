using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;

namespace Services.Features.Inventory.Commands.CreateInventory;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = InventoryMapper.ToEntity(request.Request);

        await _unitOfWork.Inventories.AddAsync(inventory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(inventory.Id);
    }
}
