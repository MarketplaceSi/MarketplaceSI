using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace MarketplaceSI.Core.Kernel.Account.Commands;

public class AccountBaseCommandHandler
{
    protected readonly UserManager<User> _userManager;
    public AccountBaseCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    protected async Task<IPrincipal> CreatePrincipal(User user)
    {
        var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var identity = new ClaimsIdentity(claims);
        return new ClaimsPrincipal(identity);
    }
}