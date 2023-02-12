using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Core.Kernel.Account.Commands;
using MarketplaceSI.Core.Kernel.Account;
using Microsoft.AspNetCore.Identity;

namespace Kernel.Accounts.Commands
{
    public class AccountRenewTokenCommandHandler : AccountBaseCommandHandler, IRequestHandler<AccountRenewTokenCommand, AccountTokenPayload>
    {

        private readonly ITokenProvider _tokenProvider;
        public AccountRenewTokenCommandHandler(UserManager<User> userManager, ITokenProvider tokenProvider, IMapper mapper) : base(userManager)
        {
            _tokenProvider = tokenProvider;
        }


        public async Task<AccountTokenPayload> Handle(AccountRenewTokenCommand request, CancellationToken cancellationToken)
        {
            var claimsPrincipal = _tokenProvider.GetPrincipalFromExpiredToken(request.AccessToken);

            var user = await _userManager.GetUserAsync(claimsPrincipal);

            if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow || user.IsDeleated)
            {
                throw new ApiException("token_invalid", System.Net.HttpStatusCode.BadRequest);
            }

            var token = _tokenProvider.CreateToken(await CreatePrincipal(user));

            user.RefreshToken = token.RefreshToken;
            await _userManager.UpdateAsync(user);

            return new AccountTokenPayload(user, token.Token, token.RefreshToken, token.RefreshTokenExpiry);
        }
    }
}
