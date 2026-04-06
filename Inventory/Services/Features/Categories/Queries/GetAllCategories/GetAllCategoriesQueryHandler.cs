using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync(cancellationToken);
        var categoryResponses = categories.Select(CategoryMapper.ToResponse).ToList();
        
        return Result.Success(categoryResponses);
    }
}
