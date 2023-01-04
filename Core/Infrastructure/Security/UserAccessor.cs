using System.IdentityModel.Tokens.Jwt;
using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Core.Domain.Services.Interfaces;
using MarketplaceSI.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MarketplaceSI.Core.Infrastructure.Security;

public class UserAccessor : IUserAccessor
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserAccessor(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
        var userId = contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        UserId = string.IsNullOrEmpty(userId) ? throw new ApiException("token_invalid") : new Guid(userId);
        Email = contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Sub)?.Value ?? throw new ApiException("token_invalid");
    }

    public Guid UserId { get; }
    public string Email { get; }

    public async Task<User> GetUser()
    {
        return await _userService.GetUserById(UserId) ?? throw new ApiException("token_invalid");
    }  
}