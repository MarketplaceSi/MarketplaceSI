using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Dto.Enums;
using MarketplaceSI.Core.Infrastructure.Repositories;
using MarketplaceSI.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class ReadOnlyProductRepository : ReadOnlyBaseRepository<Product, Guid>, IReadOnlyProductRepository
{
    public ReadOnlyProductRepository(IUnitOfWork context) : base(context)
    {
    }

    public override IQueryable<Product> GetAll()
    {
        return base.GetAll().Include(c => c.Favorited).Where(x => x.IsDeleated == false && x.Status == ProductStatus.Active);
    }

    public IQueryable<Product> GetAll(Guid? userId = null)
    {
        var products = base.GetAll().Where(x => x.IsDeleated == false && x.Status == ProductStatus.Active).Include(c => c.Category)
            .Include(c => c.Favorited.Where(x => x.UserId == userId));

        return products;
    }
}