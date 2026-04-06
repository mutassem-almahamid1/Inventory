using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id ,UpdateCategoryRequest Request) : IRequest<Result<Guid>>;
