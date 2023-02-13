using Domain.Entities;
using GreenDonut;

namespace Domain.Repositories.DataLoaders;
public interface IProductByIdDataLoader : IDataLoader<Guid, Product>
{
}