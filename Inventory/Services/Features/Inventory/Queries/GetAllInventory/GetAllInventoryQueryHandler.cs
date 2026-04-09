using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Inventory.Queries.GetAllInventory;

public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, Result<PagedResponse<InventoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllInventoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<PagedResponse<InventoryResponse>>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
    {
        var pagedResponse = await _unitOfWork.Inventories.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        
        var inventoryResponses = pagedResponse.Data.Select(InventoryMapper.ToResponse).ToList();
        
        var result = new PagedResponse<InventoryResponse>(
            inventoryResponses,
            pagedResponse.TotalCount,
            pagedResponse.PageNumber,
            pagedResponse.PageSize
        );
        
        return Result.Success(result);
    }
}
