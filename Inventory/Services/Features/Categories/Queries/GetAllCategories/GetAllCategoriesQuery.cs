using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResponse<CategoryResponse>>>;