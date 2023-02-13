using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;
public interface IReadOnlyAddressRepository : IReadOnlyBaseRepository<Address, Guid>
{
    Task<ICollection<Address>> GetUserAddressesAsync(Guid userId);
    Address GetUserDefaultAddress(Guid userId);
}
