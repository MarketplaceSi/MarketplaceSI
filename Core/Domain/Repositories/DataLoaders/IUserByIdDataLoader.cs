using GreenDonut;
using MarketplaceSI.Core.Domain.Entities;

namespace MarketplaceSI.Core.Domain.Repositories.DataLoaders;

public interface IUserByIdDataLoader : IDataLoader<Guid, User>
{
}