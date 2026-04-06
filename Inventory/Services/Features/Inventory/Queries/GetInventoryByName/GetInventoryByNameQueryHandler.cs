using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetInventoryByName;

public class GetInventoryByNameQueryHandler : IRequestHandler<GetInventoryByNameQuery, Result<InventoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInventoryByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<InventoryResponse>> Handle(GetInventoryByNameQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _unitOfWork.Inventories.GetByNameAsync(request.Name, cancellationToken);
        
        if (inventory == null)
            return Result.Failure<InventoryResponse>($"Inventory with Name {request.Name} not found.");
        
        var inventoryResponse = InventoryMapper.ToResponse(inventory);
        
        return Result.Success(inventoryResponse);
    }
    
}
