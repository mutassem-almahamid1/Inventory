using Core.Common;
using Services.Abstractions.Persistence;
using MediatR;
using Services.Mappings;

namespace Services.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _unitOfWork.Categories.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingCategory == null)
            return Result.Failure<Guid>($"Category with ID {request.Id} not found.");

        CategoryMapper.ToEntity(request.Request, existingCategory);
        
        _unitOfWork.Categories.Update(existingCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingCategory.Id);
    }
}
