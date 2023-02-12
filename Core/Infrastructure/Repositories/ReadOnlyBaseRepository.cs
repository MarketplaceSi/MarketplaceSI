using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarketplaceSI.Core.Infrastructure.Repositories;

public abstract class ReadOnlyBaseRepository<TEntity, TKey> : IReadOnlyBaseRepository<TEntity, TKey>, IDisposable
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    protected internal readonly IUnitOfWork _context;
    protected internal readonly DbSet<TEntity> _dbSet;

    public ReadOnlyBaseRepository(IUnitOfWork context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AnyAsync(predicate);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);

    public virtual IQueryable<TEntity> QueryHelper()
    {
        throw new NotImplementedException();
    }

    public virtual IQueryable<TEntity> GetAll() => _dbSet.AsQueryable();
}