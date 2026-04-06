using Core.Common;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryResponse>>;