using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetCategoryByName;

public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, Result<CategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CategoryResponse>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByNameAsync(request.Name);
        if (category == null) 
            return Result.Failure<CategoryResponse>($"Category with Name {request.Name} not found.");

        return Result.Success(CategoryMapper.ToResponse(category));
    }
}
