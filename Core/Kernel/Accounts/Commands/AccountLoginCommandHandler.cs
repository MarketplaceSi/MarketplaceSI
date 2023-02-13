using MarketplaceSI.Core.Kernel.Account.Commands;
using MarketplaceSI.Core.Kernel.Account;
using Microsoft.AspNetCore.Identity;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Accounts.Commands
{
    public class AccountLoginCommandHandler : AccountBaseCommandHandler, IRequestHandler<AccountLoginCommand, AccountTokenPayload>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserService _userService;
        private readonly IProductRepository _productRepository;
        public AccountLoginCommandHandler(UserManager<User> userManager, ITokenProvider tokenProvider, IUserService userService, IProductRepository productRepository) : base(userManager)
        {
            _tokenProvider = tokenProvider;
            _userService = userService;
            _productRepository = productRepository;
        }

        public async Task<AccountTokenPayload> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.LoginAsync(request.Email, request.Password);

            var principal = await CreatePrincipal(user);

            var token = _tokenProvider.CreateToken(principal);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiry = token.RefreshTokenExpiry;
            if (user.IsDeleated && user.DealetedAt?.AddDays(14) < DateTime.UtcNow)
            {
                user.IsDeleated = false;
                user.DealetedAt = null;
            }
            await _userManager.UpdateAsync(user);

            return new AccountTokenPayload(user, token.Token, token.RefreshToken, token.RefreshTokenExpiry);
        }
    }
}
