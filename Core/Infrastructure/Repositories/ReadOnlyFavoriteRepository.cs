using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Dto.Enums;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class ReadOnlyFavoriteRepository : IReadOnlyFavoriteRepository, IDisposable
{
    protected internal readonly IUnitOfWork _context;
    protected internal readonly DbSet<Favorite> _dbSet;

    public ReadOnlyFavoriteRepository(IUnitOfWork context)
    {
        _context = context;
        _dbSet = context.Set<Favorite>();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public IQueryable<Product> GetAll(Guid userId)
    {
        return _dbSet.Where(f => f.UserId == userId).Where(c => c.Product.Status != ProductStatus.Draft).Include(p => p.Product.Favorited.Where(x => x.UserId == userId)).Select(x => x.Product);
    }
}
