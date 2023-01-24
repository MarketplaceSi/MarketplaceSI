using GreenDonut;
using MarketplaceSI.Core.Domain.Entities;

namespace Domain.Repositories.DataLoaders;
    public interface IUserByIdDataLoader : IDataLoader<Guid, User>
    {
    }
