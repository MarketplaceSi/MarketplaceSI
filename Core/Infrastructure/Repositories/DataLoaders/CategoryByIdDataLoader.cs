using Domain.Entities;
using Domain.Repositories.DataLoaders;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceSI.Core.Infrastructure.Repositories.DataLoaders;
public class CategoryByIdDataLoader : BatchDataLoader<Guid, Category>, ICategoryByIdDataLoader
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public CategoryByIdDataLoader(IDbContextFactory<AppDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<Guid, Category>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Categories.Where(x => keys.Contains(x.Id)).Include(c => c.Categories).ToDictionaryAsync(u => u.Id, cancellationToken);
    }
}
