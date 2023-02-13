using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Infrastructure.Repositories;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class ReadOnlyProductReviewRepository : ReadOnlyBaseRepository<ProductReview, Guid>, IReadOnlyProductReviewRepository
{
    public ReadOnlyProductReviewRepository(IUnitOfWork context) : base(context)
    {
    }
}