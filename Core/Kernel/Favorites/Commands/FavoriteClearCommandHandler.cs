namespace Kernel.Favorites.Commands;
public class FavoriteClearCommandHandler : IRequestHandler<FavoriteClearCommand, ActionPayload>
{
    private readonly IUserService _userService;
    public FavoriteClearCommandHandler(IUserService userService)
    {
        _userService = userService;
    }


    public async Task<ActionPayload> Handle(FavoriteClearCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }

        user.Favorites.Clear();
        await _userService.UpdateUser(user);

        return new ActionPayload(true);
    }
}