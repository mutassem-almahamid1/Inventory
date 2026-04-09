using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResponse<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PagedResponse<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var pagedResponse = await _unitOfWork.Products.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

        var productResponses = pagedResponse.Data.Select(ProductMapper.ToResponse).ToList();
        
        var response = new PagedResponse<ProductResponse>(
            productResponses,
            pagedResponse.TotalCount,
            pagedResponse.PageNumber,
            pagedResponse.PageSize
        );
        
        return Result.Success(response);
    }
}
