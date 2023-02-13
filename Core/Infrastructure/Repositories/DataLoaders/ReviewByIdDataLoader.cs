using Domain.Entities;
using Domain.Repositories.DataLoaders;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.DataLoaders;
public class ReviewByIdDataLoader : BatchDataLoader<Guid, UserReview>, IReviewByIdDataLoader
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    private readonly Guid _reviewId;
    public ReviewByIdDataLoader(IDbContextFactory<AppDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }
    protected override async Task<IReadOnlyDictionary<Guid, UserReview>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.UserReviews.Where(x => keys.Contains(x.Id)).Include(x => x.Replays).ToDictionaryAsync(u => u.Id, cancellationToken);
    }
}