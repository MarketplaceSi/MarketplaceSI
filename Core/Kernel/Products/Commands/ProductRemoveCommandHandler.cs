using MarketplaceSI.Core.Dto.Enums;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Products.Commands;
public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, ActionPayload>
{
    private readonly IProductRepository _productRepository;

    public ProductRemoveCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    public async Task<ActionPayload> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            return new ActionPayload(true);
        }
        if (product.OwnerId != request.Id)
        {
            throw new ApiException("access_forbidden");
        }

        if (product.Status == ProductStatus.Draft)
        {
            await _productRepository.DeleteAsync(product);
            return new ActionPayload(true);
        }

        product.IsDeleated = true;
        _productRepository.Update(product);

        var res = await _productRepository.SaveChangesAsync();

        return new ActionPayload(res > 0);

    }
}