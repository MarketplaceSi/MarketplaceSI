using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Infrastructure.Repositories;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(IUnitOfWork context) : base(context)
    {

    }

}
