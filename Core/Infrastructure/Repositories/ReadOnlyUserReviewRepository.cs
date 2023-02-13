using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Infrastructure.Repositories;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class ReadOnlyUserReviewRepository : ReadOnlyBaseRepository<UserReview, Guid>, IReadOnlyUserReviewRepository
{
    public ReadOnlyUserReviewRepository(IUnitOfWork context) : base(context)
    {
    }
}