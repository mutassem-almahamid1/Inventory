using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery(): IRequest<Result<List<CategoryResponse>>>;