using Domain.Repositories.Interfaces;

namespace Kernel.Accounts.Commands;
public class AccountDeleteCommandHandler : IRequestHandler<AccountDeleteCommand, ActionPayload>
{
    private readonly IUserService _userService;
    private readonly IProductRepository _productRepository;

    public AccountDeleteCommandHandler(IUserService userService, IProductRepository productRepository)
    {
        _userService = userService;
        _productRepository = productRepository;
    }


    public async Task<ActionPayload> Handle(AccountDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            return new ActionPayload(true);
        }

        user.IsDeleated = true;
        user.DealitedReason = request.Reason;
        user.DealetedAt = DateTime.UtcNow;

        await _userService.UpdateUser(user);
        return new ActionPayload(true);
    }
}
