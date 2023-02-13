using Domain.Entities;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;
public interface IReadOnlyFavoriteRepository
{
    public IQueryable<Product> GetAll(Guid userId);
}