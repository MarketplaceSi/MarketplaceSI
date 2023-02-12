using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Infrastructure.Repositories;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class AddressRepository : BaseRepository<Address, Guid>, IAddressRepository
{
    public AddressRepository(IUnitOfWork context) : base(context)
    {
    }
}
