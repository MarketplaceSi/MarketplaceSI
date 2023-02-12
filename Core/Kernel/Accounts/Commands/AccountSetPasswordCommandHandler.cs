using MarketplaceSI.Core.Infrastructure.Extensions;

namespace Kernel.Accounts.Commands;
public class AccountSetPasswordCommandHandler : IRequestHandler<AccountSetPasswordCommand, ActionPayload>
{
    private readonly IUserService _userService;
    public AccountSetPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }


    public async Task<ActionPayload> Handle(AccountSetPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = null;
        if (RandomExtension.IsKey(request.Key))
        {
            user = await _userService.CompletePasswordReset(request.Password, request.Key);
        }
        else
        {
            if (request.Key.Equals(request.Password))
            {
                throw new ApiException("password_new_old", System.Net.HttpStatusCode.BadRequest);
            }
            user = await _userService.ChangePassword(request.Key, request.Password);
        }
        //TODO: Send email to the user about password reseting?
        return new ActionPayload(user is not null);
    }
}
