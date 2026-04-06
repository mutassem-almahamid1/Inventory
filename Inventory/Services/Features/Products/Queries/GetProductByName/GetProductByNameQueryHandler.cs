

using Core.Common;
using MediatR;
using Services.Abstractions.Persistence;
using Services.Mappings;
using Shared.Responses;

namespace Services.Features.Products.Queries.GetProductByName;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Result<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByNameAsync(request.Name);
        if (product is null)
            return Result.Failure<ProductResponse>($"Product with Name {request.Name} not found.");
        
        return Result.Success(ProductMapper.ToResponse(product));
    }
}
