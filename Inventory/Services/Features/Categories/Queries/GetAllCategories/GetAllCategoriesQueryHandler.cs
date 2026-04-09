using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<PagedResponse<CategoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<PagedResponse<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var pagedResponse = await _unitOfWork.Categories.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var categoryResponses = pagedResponse.Data.Select(CategoryMapper.ToResponse).ToList();
        
        var result = new PagedResponse<CategoryResponse>(
            categoryResponses,
            pagedResponse.TotalCount,
            pagedResponse.PageNumber,
            pagedResponse.PageSize
        );
        
        return Result.Success(result);
    }
}
