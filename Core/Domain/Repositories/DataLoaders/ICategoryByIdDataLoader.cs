using Domain.Entities;
using GreenDonut;

namespace Domain.Repositories.DataLoaders;
public interface ICategoryByIdDataLoader : IDataLoader<Guid, Category>
{
}