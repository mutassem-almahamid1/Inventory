using Core.Common;
using Services.Abstractions.Persistence;
using MediatR;
using Services.Mappings;

namespace Services.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingProduct == null)
            return Result.Failure<Guid>($"Product with ID {request.Id} not found.");

        ProductMapper.ToEntity(request.Request, existingProduct);
        
        _unitOfWork.Products.Update(existingProduct);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(existingProduct.Id);
    }
}
