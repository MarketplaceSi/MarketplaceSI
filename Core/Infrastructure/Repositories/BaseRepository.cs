using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Core.Infrastructure.Repositories;

public class BaseRepository<TEntity, TKey> : ReadOnlyBaseRepository<TEntity, TKey>, IBaseRepository<TEntity, TKey>, IDisposable
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    public BaseRepository(IUnitOfWork context) : base(context)
    {
    }

    public TEntity Add(TEntity entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public bool AddRange(params TEntity[] entities)
    {
        _dbSet.AddRange(entities);
        return true;
    }
    public bool UpdateRange(params TEntity[] entities)
    {
        _dbSet.UpdateRange(entities);
        return true;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await Task.FromResult(_dbSet.Remove(entity));
    }

    public async Task DeleteByIdAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public TEntity Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public bool UpdateRange(IQueryable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        return true;
    }
}