using Core.Common;
using Services.Abstractions.Persistence;
using MediatR;
using Services.Mappings;

namespace Services.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = CategoryMapper.ToEntity(request.Request);
        
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(category.Id);
    }
}
