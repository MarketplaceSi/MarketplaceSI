using Domain.Entities;

namespace Kernel.Favorites.Queries;
public class FavoriteListQueryHandler : IRequestHandler<FavoriteListQuery, IQueryable<Product>>
{
    public readonly IUserService _userService;
    private readonly IReadOnlyFavoriteRepository _favoriteRepository;


    public FavoriteListQueryHandler(IUserService userService, IReadOnlyFavoriteRepository favoriteRepository)
    {
        _userService = userService;
        _favoriteRepository = favoriteRepository;
    }


    public async Task<IQueryable<Product>> Handle(FavoriteListQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }
        return _favoriteRepository.GetAll(user.Id);
    }
}