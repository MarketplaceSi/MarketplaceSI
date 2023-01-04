using System.Security.Claims;
using System.Threading.Tasks;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace MarketplaceSI.Core.Infrastructure.Security;

public class RoleClaimsTransformation : IClaimsTransformation
{
    private readonly ITokenProvider _tokenProvider;

    public RoleClaimsTransformation(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        return Task.FromResult(_tokenProvider.TransformPrincipal(principal));
    }
}