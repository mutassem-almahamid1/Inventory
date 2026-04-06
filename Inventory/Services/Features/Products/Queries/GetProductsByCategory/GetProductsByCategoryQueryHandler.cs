using Core.Common;
using Services.Abstractions.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Responses;
using Services.Mappings;

namespace Services.Features.Products.Queries.GetProductsByCategory;

public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<List<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetProductsByCategoryQueryHandler> _logger;

    public GetProductsByCategoryQueryHandler(IUnitOfWork unitOfWork, ILogger<GetProductsByCategoryQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<ProductResponse>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to retrieve products for CategoryId: {CategoryId}", request.CategoryId);

        var products = await _unitOfWork.Products.GetByCategoryIdAsync(request.CategoryId);
        _logger.LogInformation("Found {Count} products for CategoryId: {CategoryId}", products.Count, request.CategoryId);

        var productResponses = products.Select(ProductMapper.ToResponse).ToList();

        return Result.Success(productResponses);
    }
}
