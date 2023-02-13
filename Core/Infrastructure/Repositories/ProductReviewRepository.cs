using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class ProductReviewRepository : BaseRepository<ProductReview, Guid>, IProductReviewRepository
{
    public ProductReviewRepository(IUnitOfWork context) : base(context)
    {
    }
}
