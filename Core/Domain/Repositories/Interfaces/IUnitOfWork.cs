using Microsoft.EntityFrameworkCore;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    DbSet<T> Set<T>(string? name = null) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> ExecuteSqlAsync(string request, CancellationToken cancellationToken = default);
}