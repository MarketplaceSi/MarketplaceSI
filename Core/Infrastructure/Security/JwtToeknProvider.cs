using MarketplaceSI.Core.Domain.Constants;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Dto.Enums;
using MarketplaceSI.Core.Dto.Users;
using MarketplaceSI.Core.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace MarketplaceSI.Core.Infrastructure.Security;

public class JwtTokenProvider : ITokenProvider
{
    private const string AuthoritiesKey = "auth";
    private readonly SecuritySettings _securitySettings;
    private readonly SymmetricSecurityKey _key;
    private readonly SymmetricSecurityKey _verifyKey;
    private int _tokenExpirationInSeconds { get; set; }
    private int _verifyTokenExpirationInSeconds { get; set; }
    private int _refreshTokenExpirationInSeconds { get; set; }
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtTokenProvider(IOptions<SecuritySettings> opt)
    {
        _securitySettings = opt.Value;
        _key = new SymmetricSecurityKey(Convert.FromBase64String(_securitySettings.Authentication.Jwt.Base64Secret));
        _verifyKey = new SymmetricSecurityKey(Convert.FromBase64String(_securitySettings.Authentication.Jwt.Verify64Secret));
        _tokenExpirationInSeconds = _securitySettings.Authentication.Jwt.TokenExpirationInSeconds;
        _verifyTokenExpirationInSeconds = _securitySettings.Authentication.Jwt.VerifyTokenExpirationInSeconds;
        _refreshTokenExpirationInSeconds = _securitySettings.Authentication.Jwt.RefreshTokenExpirationInDays;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public TokenDto CreateToken(IPrincipal principal)
    {        
        return new TokenDto(CreateToken(principal, Constants.JWTKey.SigningKey), GenerateRefreshToken(), DateTime.UtcNow.AddSeconds(_refreshTokenExpirationInSeconds));
    }

    private string CreateToken(IPrincipal principal,string key)
    {
        var subject = key == Constants.JWTKey.SigningKey ? CreateSubject(principal) 
            : new ClaimsIdentity(principal.Identity);
        var validity = key == Constants.JWTKey.SigningKey ? DateTime.UtcNow.AddSeconds(_tokenExpirationInSeconds)
            : DateTime.UtcNow.AddSeconds(_verifyTokenExpirationInSeconds);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _securitySettings.Authentication.Jwt.Audience,
            Issuer = _securitySettings.Authentication.Jwt.Issuer,
            Subject = subject,
            Expires = validity,
            SigningCredentials = new SigningCredentials(GetSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }
    private SymmetricSecurityKey GetSecurityKey(string key)
    {
        switch (key)
        {
            case Constants.JWTKey.VerifyKey:
                return _verifyKey;
            default:
                return _key;
        }       
    }

    public ClaimsPrincipal TransformPrincipal(ClaimsPrincipal principal)
    {
        var currentIdentity = (ClaimsIdentity)principal.Identity;
        var roleClaims = principal
            .Claims
            .Where(it => it.Type == AuthoritiesKey).First().Value
            .Split(",")
            .Select(role => new Claim(ClaimTypes.Role, role))
            .ToList();

        return new ClaimsPrincipal(
            new ClaimsIdentity(
                principal.Claims.Union(roleClaims),
                currentIdentity.AuthenticationType,
                currentIdentity.NameClaimType,
                currentIdentity.RoleClaimType
            )
        );
    }

    private static ClaimsIdentity CreateSubject(IPrincipal principal)
    {
        var userName = principal.Identity?.Name;
        var userId = GetId(principal);
        var roles = GetRoles(principal);
        var authValue = string.Join(",", roles.Select(it => it.Value));
        return new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId?.Value ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, userName ?? ""),
                new Claim(AuthoritiesKey, authValue)
            });
    }

    private static IEnumerable<Claim> GetRoles(IPrincipal principal)
    {
        return principal is ClaimsPrincipal user
            ? user.FindAll(it => it.Type == ClaimTypes.Role)
            : Enumerable.Empty<Claim>();
    }

    private static Claim? GetId(IPrincipal principal)
    {
        return principal is ClaimsPrincipal user
            ? user.FindFirst(it => it.Type == ClaimTypes.NameIdentifier)
            : null;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string key = null)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = _securitySettings.Authentication.Jwt.Issuer,
            ValidAudience = _securitySettings.Authentication.Jwt.Audience,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetSecurityKey(key),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    public ClaimsPrincipal ValidateToken(string token, string key = null)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return principal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string CreateToken(string name, string email, EmailTemplates templates, string key )
    {

        var claims = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.UserData, templates.ToDescription())
        });
        var token = CreateToken(new ClaimsPrincipal(claims), key); 
        return token;
    }

   
}