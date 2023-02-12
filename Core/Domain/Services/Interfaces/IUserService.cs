using MarketplaceSI.Core.Domain.Entities;

namespace MarketplaceSI.Core.Domain.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUser(User userToCreate);
    IEnumerable<string> GetAuthorities();
    Task DeleteUser(string login);
    Task<User> UpdateUser(User userToUpdate);
    Task<User?> CompletePasswordReset(string newPassword, string key);
    Task<User?> RequestPasswordReset(string mail);
    Task<User?> RequestEmailVerify(string mail);
    Task<User?> ChangePassword(string currentClearTextPassword, string newPassword);
    Task<User?> ActivateRegistration(string key);
    Task<User> RegisterUser(User userToRegister, string password);
    Task UpdateUser(string firstName, string lastName, string email, string langKey, string imageUrl);
    Task<User?> GetUserWithUserRoles();
    Task<User?> GetUserAsync();
    Task<bool> VerifyUserEmail(string email, string verifikationKey);
    Task<User?> GetUserById(Guid id);
    Task<User> LoginAsync(string email, string password);
}
