using Domain.Entities;
using Infrastructure.Data;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Core.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
    {
        public ProductRepository(IUnitOfWork context) : base(context)
        {

        }

        public async Task DeleteUserProductsAsync(Guid userId, bool delete = true)
        {
            var req = $"UPDATE public.\"{nameof(AppDbContext.Products)}\" SET \"{nameof(Product.IsDeleated)}\"='{delete.ToString()}' WHERE \"{nameof(Product.OwnerId)}\"='{userId.ToString()}';";
            await _context.ExecuteSqlAsync(req);
        }
    }
}
