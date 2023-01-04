using Infrastructure.Data;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceSI.Core.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    protected readonly DbContext _context;
    public UnitOfWork(IDbContextFactory<AppDbContext> context)
    {
        _context = context.CreateDbContext();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> ExecuteSqlAsync(string request, CancellationToken cancellationToken = default)
    {
        return await _context.Database.ExecuteSqlRawAsync(request, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    public DbSet<T> Set<T>(string? name = null) where T : class => _context.Set<T>(name);  
}