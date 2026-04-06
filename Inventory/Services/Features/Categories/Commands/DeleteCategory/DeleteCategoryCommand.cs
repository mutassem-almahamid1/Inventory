using Core.Common;
using MediatR;

namespace Services.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Result<Guid>>;
