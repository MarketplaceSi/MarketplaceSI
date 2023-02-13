using Kernel.Products;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Favorites.Commands;
public class FavoriteRemoveCommandHandler : IRequestHandler<FavoriteRemoveCommand, ProductPayloadBase>
{
    private readonly IUserService _userService;
    private readonly IProductRepository _productRepository;

    public FavoriteRemoveCommandHandler(IUserService userService, IProductRepository productRepository)
    {
        _userService = userService;
        _productRepository = productRepository;
    }


    public async Task<ProductPayloadBase> Handle(FavoriteRemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }

        var fav = user.Favorites.FirstOrDefault(x => x.ProductId == request.Id);

        var product = fav?.Product;

        if (product != null)
        {
            user.Favorites.Remove(fav);
            await _userService.UpdateUser(user);
        }
        else
        {
            product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new ApiException("product_not_exists");
            }
        }

        product.Favorited = product.Favorited.Where(x => x.UserId == user.Id).ToList();
        return new ProductPayloadBase(product);
    }
}
