using System.Linq.Expressions;
using MarketplaceSI.Core.Domain.Entities;
namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;

//TODO: Add pagination

public interface IReadOnlyBaseRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> QueryHelper();
    public IQueryable<TEntity> GetAll();
}