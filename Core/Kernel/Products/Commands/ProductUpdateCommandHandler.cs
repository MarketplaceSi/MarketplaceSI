using Infrastructure.Extensions;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Products.Commands;
public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, ProductPayloadBase>
{
    private readonly IProductRepository _productRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IStorageService _storageService;
    public ProductUpdateCommandHandler(IProductRepository productRepository, IUserAccessor userAccessor, IStorageService storageService)
    {
        _productRepository = productRepository;
        _userAccessor = userAccessor;
        _storageService = storageService;
    }


    public async Task<ProductPayloadBase> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            throw new ApiException("product_not_exists");
        }
        if (product.OwnerId != _userAccessor.UserId)
        {
            throw new ApiException("access_forbidden");
        }
        if (request.FilesToDelete != null || request.FilesToDelete.Any())
        {
            await _storageService.DeleteObjectsAsync(request.FilesToDelete.Select(f => f.GetFileName()).ToList());
        }
        if (request.FilesToUpload != null && request.FilesToUpload.Any())
        {
            product.Pictures = await _storageService.UploadImagesAsync(request.FilesToUpload, product.GetPath());
        }
        product.CategoryId = request.CategoryId;
        product.Title = request.Title;
        product.Description = request.Description;
        product.Condition = request.Condition;
        product.Price = request.Price;

        product = _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
        return new ProductPayloadBase(product);
    }
}
