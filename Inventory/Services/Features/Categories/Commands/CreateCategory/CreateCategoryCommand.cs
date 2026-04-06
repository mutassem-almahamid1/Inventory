using Core.Common;
using MediatR;
using Shared.Requests;

namespace Services.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<Result<Guid>>;