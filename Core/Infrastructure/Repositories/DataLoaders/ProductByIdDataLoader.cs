using Domain.Entities;
using Domain.Repositories.DataLoaders;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Repositories.DataLoaders;
public class ProductByIdDataLoader : BatchDataLoader<Guid, Product>, IProductByIdDataLoader
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    private readonly Guid _userId;
    public ProductByIdDataLoader(IDbContextFactory<AppDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IHttpContextAccessor contextAccessor) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        var idClaim = contextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Sub))?.Value;
        if (!string.IsNullOrEmpty(idClaim))
        {
            Guid.TryParse(idClaim, out _userId);
        }
    }

    protected override async Task<IReadOnlyDictionary<Guid, Product>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Products
            .Where(x => keys.Contains(x.Id))
            .Include(c => c.Owner)
            .Include(_ => _.Category)
            .Include(_ => _.Favorited.Where(x => x.UserId == _userId))
            .ToDictionaryAsync(u => u.Id, cancellationToken);
    }
}