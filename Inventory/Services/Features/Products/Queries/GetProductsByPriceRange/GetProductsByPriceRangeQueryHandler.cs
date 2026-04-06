using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductsByPriceRange;

public class GetProductsByPriceRangeQueryHandler : IRequestHandler<GetProductsByPriceRangeQuery, Result<List<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsByPriceRangeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<ProductResponse>>> Handle(GetProductsByPriceRangeQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetByPriceRangeAsync(request.MinPrice, request.MaxPrice, cancellationToken);

        var productResponses = products.Select(ProductMapper.ToResponse).ToList();
        
        return Result.Success(productResponses);
    }
}
