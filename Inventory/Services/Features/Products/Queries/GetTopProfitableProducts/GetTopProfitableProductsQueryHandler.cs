using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetTopProfitableProducts;

public class GetTopProfitableProductsQueryHandler : IRequestHandler<GetTopProfitableProductsQuery, Result<List<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTopProfitableProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<ProductResponse>>> Handle(GetTopProfitableProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetTopProfitableAsync(request.Count, cancellationToken);

        var productResponses = products.Select(ProductMapper.ToResponse).ToList();

        return Result.Success(productResponses);
    }
}
