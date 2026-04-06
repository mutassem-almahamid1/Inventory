using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetLowStockInventory;

public class GetLowStockInventoryQueryHandler : IRequestHandler<GetLowStockInventoryQuery, Result<List<InventoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLowStockInventoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<InventoryResponse>>> Handle(GetLowStockInventoryQuery request, CancellationToken cancellationToken)
    {
        var inventories = await _unitOfWork.Inventories.GetAllAsync(cancellationToken);

        var lowStockInventories = inventories
            .Where(inventory => inventory.Quantity < inventory.ReorderLevel)
            .Select(InventoryMapper.ToResponse)
            .ToList();

        return Result.Success(lowStockInventories);
    }
}


