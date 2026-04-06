using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByInventory;

public class GetProductsByInventoryQueryHandler : IRequestHandler<GetProductsByInventoryQuery, Result<List<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByInventoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Result<List<ProductResponse>>> Handle(GetProductsByInventoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetProductsByInventoryAsync(request.Id, cancellationToken);

        var productResponses = products.Select(ProductMapper.ToResponse).ToList();

        return Result.Success(productResponses);
    }
}
