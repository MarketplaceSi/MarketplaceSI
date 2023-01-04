using MarketplaceSI.Core.Domain.Entities;

namespace MarketplaceSI.Core.Domain.Security.Interfaces;

public interface IUserAccessor 
{

    Guid UserId { get; }
    string Email { get; }
    Task<User> GetUser();
}