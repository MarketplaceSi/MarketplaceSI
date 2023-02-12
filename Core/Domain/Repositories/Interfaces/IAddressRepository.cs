using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;
public interface IAddressRepository : IBaseRepository<Address, Guid>
{
}