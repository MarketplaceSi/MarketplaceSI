using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class FavoriteRepository : IFavoriteRepository, IDisposable
{
    protected internal readonly IUnitOfWork _context;
    protected internal readonly DbSet<Favorite> _dbSet;

    public FavoriteRepository(IUnitOfWork context)
    {
        _context = context;
        _dbSet = context.Set<Favorite>();
    }


    public async Task<bool> Exists(Expression<Func<Favorite, bool>> predicate)
            => await _dbSet.AnyAsync(predicate);

    public Favorite Add(Favorite favorite)
    {
        _dbSet.Add(favorite);
        return favorite;
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

}
