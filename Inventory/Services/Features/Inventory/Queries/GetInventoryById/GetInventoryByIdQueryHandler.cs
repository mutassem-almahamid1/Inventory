using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetInventoryById;

public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, Result<InventoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInventoryByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<InventoryResponse>> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _unitOfWork.Inventories.GetByIdAsync(request.Id, cancellationToken);
        
        if (inventory == null)
            return Result.Failure<InventoryResponse>($"Inventory with ID {request.Id} not found.");
        
        var inventoryResponse = InventoryMapper.ToResponse(inventory);
        
        return Result.Success(inventoryResponse);
    }
    
}
