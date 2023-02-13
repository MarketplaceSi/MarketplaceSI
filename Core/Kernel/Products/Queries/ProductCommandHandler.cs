using Domain.Entities;
using Domain.Repositories.DataLoaders;
using MarketplaceSI.Core.Dto.Enums;
using MarketplaceSI.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Kernel.Products.Queries;
public class ProductCommandHandler : IRequestHandler<ProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductByIdDataLoader _productByIdDataLoader;
    private readonly IHttpContextAccessor _contextAccessor;

    public ProductCommandHandler(IProductRepository productRepository, IHttpContextAccessor contextAccessor, IProductByIdDataLoader productDataLoader)
    {
        _productRepository = productRepository;
        _contextAccessor = contextAccessor;
        _productByIdDataLoader = productDataLoader;
    }


    public async Task<Product> Handle(ProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productByIdDataLoader.LoadAsync(request.Id);
        if (product is null || product.IsDeleated)
        {
            throw new ApiException("product_not_exists");
        }

        var userId = _contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (product is not { Status: ProductStatus.Active })
        {
            if (string.IsNullOrEmpty(userId) || product.OwnerId != Guid.Parse(userId))
            {
                throw new ApiException("access_forbidden");
            }
        }
        return product;
    }
}