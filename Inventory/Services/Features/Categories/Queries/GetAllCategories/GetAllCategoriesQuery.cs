using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<CategoryResponse>>>, ICacheableQuery
{
    public string CacheKey => $"Categories-All-{PageNumber}-{PageSize}";
    public int ExpirationInSeconds => 300;
    public bool BypassCache => false;
}