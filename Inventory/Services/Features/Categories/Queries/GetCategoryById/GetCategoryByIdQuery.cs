using Core.Common;
using Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace Services.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryResponse>>, ICacheableQuery
{
    public string CacheKey => $"Category-{Id}";
    public int ExpirationInSeconds => 600;
    public bool BypassCache => false;
}