using Domain.Entities;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Core.Dto.Enums;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Products.Commands;
public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductPayloadBase>
{
    private readonly IProductRepository _productRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IStorageService _storageService;

    public ProductCreateCommandHandler(IProductRepository productRepository, IUserAccessor userAccessor, IStorageService storageService)
    {
        _productRepository = productRepository;
        _userAccessor = userAccessor;
        _storageService = storageService;
    }

    public async Task<ProductPayloadBase> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = _productRepository.Add(new Product()
        {
            CategoryId = request.CategoryId,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            Status = ProductStatus.Draft,
            OwnerId = _userAccessor.UserId,
        });
        product.Pictures = await _storageService.UploadImagesAsync(request.Files, product.GetPath());
        //_productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
        return new ProductPayloadBase(product);
    }
}