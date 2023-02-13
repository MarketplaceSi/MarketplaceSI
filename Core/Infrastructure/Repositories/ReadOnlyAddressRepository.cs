using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class ReadOnlyAddressRepository : ReadOnlyBaseRepository<Address, Guid>, IReadOnlyAddressRepository
{
    public ReadOnlyAddressRepository(IUnitOfWork context) : base(context)
    {
    }

    public async Task<ICollection<Address>> GetUserAddressesAsync(Guid userId)
        => await _dbSet.Where(x => x.UserId == userId).ToListAsync();

    public Address GetUserDefaultAddress(Guid userId)
        => _dbSet.FirstOrDefault(x => x.UserId == userId && x.IsDefault == true);
}