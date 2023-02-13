using MarketplaceSI.Core.Domain.Entities;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;

public interface IBaseRepository<TEntity, TKey> : IReadOnlyBaseRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    Task DeleteByIdAsync(TKey id);
    Task DeleteAsync(TEntity entity);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    bool UpdateRange(params TEntity[] entities);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    bool AddRange(params TEntity[] entities);
    bool UpdateRange(IQueryable<TEntity> entities);

}