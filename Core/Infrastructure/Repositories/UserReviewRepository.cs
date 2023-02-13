using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;
using MarketplaceSI.Core.Infrastructure.Repositories;

namespace MarketplaceSI.Core.Infrastructure.Repositories;
public class UserReviewRepository : BaseRepository<UserReview, Guid>, IUserReviewRepository
{
    public UserReviewRepository(IUnitOfWork context) : base(context)
    {
    }
}