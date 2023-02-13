using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Domain.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product, Guid>
    {
        Task DeleteUserProductsAsync(Guid userId, bool delete = true);
    }
}
