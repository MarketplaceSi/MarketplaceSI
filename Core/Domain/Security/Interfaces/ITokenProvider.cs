using MarketplaceSI.Core.Dto.Enums;
using MarketplaceSI.Core.Dto.Users;
using System.Security.Claims;
using System.Security.Principal;

namespace MarketplaceSI.Core.Domain.Security.Interfaces;

public interface ITokenProvider
{
    TokenDto CreateToken(IPrincipal principal);
    ClaimsPrincipal TransformPrincipal(ClaimsPrincipal principal);
    ClaimsPrincipal ValidateToken(string token, string key = null);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string key = null);
    string CreateToken(string name, string email, EmailTemplates emailTemplates, string key);

}