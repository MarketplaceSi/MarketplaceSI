using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Domain.Repositories.Interfaces;
public interface IReadOnlyProductRepository : IReadOnlyBaseRepository<Product, Guid>
{
    IQueryable<Product> GetAll(Guid? userId = null);
}