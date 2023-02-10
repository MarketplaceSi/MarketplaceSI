using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using MarketplaceSI.Core.Domain.Entities;
using Domain.Repositories.DataLoaders;

namespace Infrastructure.Repositories.DataLoaders
{
    public class UserByIdDataLoader : BatchDataLoader<Guid, User>, IUserByIdDataLoader
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public UserByIdDataLoader(IDbContextFactory<AppDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options) : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, User>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await dbContext.Users
                .Where(x => keys.Contains(x.Id) && x.IsDeleated == false)
                .Include(u => u.Addresses)
                .ToDictionaryAsync(u => u.Id, cancellationToken);
        }
    }
}
