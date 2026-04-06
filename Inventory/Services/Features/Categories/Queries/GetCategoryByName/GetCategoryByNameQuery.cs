using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetCategoryByName;

public record GetCategoryByNameQuery(string Name) : IRequest<Result<CategoryResponse>>;
