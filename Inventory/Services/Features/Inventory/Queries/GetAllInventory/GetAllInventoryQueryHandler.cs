using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetAllInventory;

public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, Result<List<InventoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllInventoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<InventoryResponse>>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
    {
        var inventories = await _unitOfWork.Inventories.GetAllAsync(cancellationToken);
        
        var inventoryResponses = inventories.Select(InventoryMapper.ToResponse).ToList();
        
        return Result.Success(inventoryResponses);
    }
}
