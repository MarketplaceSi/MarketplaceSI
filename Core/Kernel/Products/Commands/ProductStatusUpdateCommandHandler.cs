using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Products.Commands;
public class ProductStatusUpdateCommandHandler : IRequestHandler<ProductStatusUpdateCommand, ProductPayloadBase>
{
    private readonly IProductRepository _productRepository;
    private readonly IUserAccessor _userAccesor;

    public ProductStatusUpdateCommandHandler(IProductRepository productRepository, IUserAccessor userAccessor, IMapper mapper)
    {
        _productRepository = productRepository;
        _userAccesor = userAccessor;
    }


    public async Task<ProductPayloadBase> Handle(ProductStatusUpdateCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAccesor.UserId;
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null || product.OwnerId != userId)
        {
            throw new ApiException("access_forbidden");
        }
        product.Status = request.Status;

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();

        return new ProductPayloadBase(product);
    }
}
