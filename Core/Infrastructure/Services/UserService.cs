using System.IdentityModel.Tokens.Jwt;
using MarketplaceSI.Core.Domain.Constants;
using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Services.Interfaces;
using MarketplaceSI.Core.Infrastructure.Exceptions;
using MarketplaceSI.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarketplaceSI.Core.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UserService> _log;
    public UserService(ILogger<UserService> logger, UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, RoleManager<Role> roleManager,
            IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        _log = logger;
    }

    public virtual async Task<User?> ActivateRegistration(string key)
    {
        _log.LogDebug($"Activating user for activation key {key}");
        var user = await _userManager.Users.SingleOrDefaultAsync(it => it.ActivationKey == key);
        if (user == null)
            return null;

        user.Activated = true;
        user.ActivationKey = null;
        await _userManager.UpdateAsync(user);

        _log.LogDebug($"Activated user: {user}");

        return user;
    }

    public virtual async Task<User?> ChangePassword(string currentClearTextPassword, string newPassword)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user != null)
        {
            var currentEncryptedPassword = user.PasswordHash;
            var isInvalidPassword =
                _passwordHasher.VerifyHashedPassword(user, currentEncryptedPassword, currentClearTextPassword) !=
                PasswordVerificationResult.Success;

            if (isInvalidPassword)
                throw new ApiException("incorrect_password", System.Net.HttpStatusCode.BadRequest);

            var encryptedPassword = _passwordHasher.HashPassword(user, newPassword);
            user.PasswordHash = encryptedPassword;
            await _userManager.UpdateAsync(user);
            _log.LogDebug($"Changed password for User: {user}");
        }

        return user;
    }

    public virtual async Task<User?> CompletePasswordReset(string newPassword, string key)
    {
        _log.LogDebug($"Reset user password for reset key {key}");
        var user = await _userManager.Users.SingleOrDefaultAsync(it => it.ResetKey == key);
        if (user == null || user.ResetDate <= DateTime.Now.Subtract(TimeSpan.FromSeconds(86400)))
            throw new ApiException("token_expired", System.Net.HttpStatusCode.BadRequest);

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
        user.ResetKey = null;
        user.ResetDate = null;
        if (!user.EmailConfirmed)
        {   
            user.EmailConfirmed = true;
            user.Activated = true;
        }
        await _userManager.UpdateAsync(user);
        return user;
    }

    public virtual async Task<User> CreateUser(User userToCreate)
    {
        var user = new User
        {
            UserName = userToCreate.Email.ToLower(),
            FirstName = userToCreate.FirstName,
            LastName = userToCreate.LastName,
            Email = userToCreate.Email.ToLower(),
            Avatar = userToCreate.Avatar,
            LangKey = userToCreate.LangKey ?? Constants.DefaultLangKey,
            PasswordHash = _userManager.PasswordHasher.HashPassword(null, RandomExtension.GeneratePassword()),
            ResetKey = RandomExtension.GenerateResetKey(),
            ResetDate = DateTime.Now,
            Activated = true
        };
        await _userManager.CreateAsync(user);
        await CreateUserRoles(user, userToCreate.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
        return user;
    }

    public virtual async Task DeleteUser(string login)
    {
        var user = await _userManager.FindByNameAsync(login);
        if (user != null)
        {
            await DeleteUserRoles(user);
            await _userManager.DeleteAsync(user);
        }
    }

    public virtual IEnumerable<string> GetAuthorities()
    {
        return _roleManager.Roles.Select(it => it.Name).AsQueryable();
    }

    public virtual async Task<User?> GetUserAsync()
    {
        return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
    }

    public virtual async Task<User?> GetUserWithUserRoles()
    {
        var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext?.User);
        if (userName == null)
            return null;
        return await GetUserWithUserRolesByName(userName);
    }

    public virtual async Task<User> RegisterUser(User userToRegister, string password)
    {
        var existingUser = _userManager.Users.FirstOrDefault(it => it.NormalizedEmail == userToRegister.Email.ToUpper());
        if (existingUser != null)
        {
            var removed = await RemoveNonActivatedUser(existingUser);
            if (!removed)
                throw new ApiException("user_already_exists", System.Net.HttpStatusCode.BadRequest);
        }

        var newUser = new User
        {
            UserName = userToRegister.Email.ToLower(),
            // // new user gets initially a generated password
            // PasswordHash = _passwordHasher.HashPassword(null, password),
            FirstName = userToRegister.FirstName,
            LastName = userToRegister.LastName,
            Email = userToRegister.Email.ToLowerInvariant(),
            Avatar = userToRegister.Avatar,
            LangKey = userToRegister.LangKey,
            // new user is not active
            //TODO: Add email verefication and user activation
            Activated = false,
            EmailConfirmed = false,
            ResetKey = RandomExtension.GenerateResetKey(),
            ResetDate = DateTime.Now
            //TODO manage authorities
        };
        await _userManager.CreateAsync(newUser);
        _log.LogDebug($"Created Information for User: {newUser}");
        return newUser;
    }

    public virtual async Task<User?> RequestPasswordReset(string mail)
    {
        var user = await _userManager.FindByEmailAsync(mail);
        if (user == null)
            return null;

        user.ResetKey = RandomExtension.GenerateResetKey();
        user.ResetDate = DateTime.Now;
        await _userManager.UpdateAsync(user);
        return user;
    }

    public virtual async Task<User?> RequestEmailVerify(string mail)
    {
        var user = await _userManager.FindByEmailAsync(mail);
        if (user == null )
            return null;

        if (user.EmailConfirmed)
        {
            throw new ApiException("email_confirmed", System.Net.HttpStatusCode.BadRequest);
        }

        user.ResetKey = RandomExtension.GenerateResetKey();
        user.ResetDate = DateTime.Now;
        await _userManager.UpdateAsync(user);
        return user;
    }

    public virtual async Task<User> UpdateUser(User userToUpdate)
    {
        var user = await _userManager.FindByIdAsync(userToUpdate.Id.ToString());
        user.UserName = userToUpdate.Email.ToLower();
        user.FirstName = userToUpdate.FirstName;
        user.LastName = userToUpdate.LastName;
        user.Email = userToUpdate.Email;
        user.Avatar = userToUpdate.Avatar;
        user.Activated = userToUpdate.Activated;
        user.LangKey = userToUpdate.LangKey;
        await _userManager.UpdateAsync(user);
        await UpdateUserRoles(user, userToUpdate.UserRoles.Select(iur => iur.Role.Name).ToHashSet());
        return user;
    }

    public virtual async Task UpdateUser(string firstName, string lastName, string email, string langKey, string imageUrl)
    {
        var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext?.User);
        var user = await _userManager.FindByNameAsync(userName);
        if (user != null)
        {
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.LangKey = langKey;
            user.Avatar = imageUrl;
            await _userManager.UpdateAsync(user);
            _log.LogDebug($"Changed Information for User: {user}");
        }
    }


    private async Task<User?> GetUserWithUserRolesByName(string name)
    {
        return await _userManager.Users
            .Include(it => it.UserRoles)
            .ThenInclude(r => r.Role)
            .SingleOrDefaultAsync(it => it.UserName == name);
    }

    private async Task<bool> RemoveNonActivatedUser(User existingUser)
    {
        if (existingUser.Activated) return false;

        await _userManager.DeleteAsync(existingUser);
        return true;
    }

    private async Task CreateUserRoles(User user, IEnumerable<string> roles)
    {
        if (roles == null || !roles.Any()) return;

        foreach (var role in roles) await _userManager.AddToRoleAsync(user, role);
    }

    private async Task UpdateUserRoles(User user, IEnumerable<string> roles)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var rolesToRemove = userRoles.Except(roles).ToArray();
        var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();
        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        await _userManager.AddToRolesAsync(user, rolesToAdd);
    }

    private async Task DeleteUserRoles(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
    }

    public virtual async Task<bool> VerifyUserEmail(string email, string verifikationKey)
    {
        return true;
    }
    public virtual async Task<User?> GetUserById(Guid id) => await _userManager.Users.FirstAsync(x => x.Id == id);
}
