using Kernel.Products;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Favorites.Commands;
public class FavoriteAddCommandHandler : IRequestHandler<FavoriteAddCommand, ProductPayloadBase>
{
    private readonly IUserService _userService;
    private readonly IProductRepository _productRepository;

    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteAddCommandHandler(IUserService userService, IProductRepository productRepository, IFavoriteRepository favoriteRepository)
    {
        _userService = userService;
        _productRepository = productRepository;
        _favoriteRepository = favoriteRepository;
    }


    public async Task<ProductPayloadBase> Handle(FavoriteAddCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }

        if (await _favoriteRepository.Exists(x => x.ProductId == request.Id && x.UserId == user.Id))
        {
            throw new ApiException("already_in_favorited");
        }

        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ApiException("product_not_exists");
        }

        _favoriteRepository.Add(new Domain.Entities.Favorite()
        {
            ProductId = product.Id,
            UserId = user.Id
        });
        await _favoriteRepository.SaveChangesAsync();
        product.Favorited = product.Favorited.Where(x => x.UserId == user.Id).ToList();
        return new ProductPayloadBase(product);
    }
}