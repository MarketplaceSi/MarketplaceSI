using Domain.Entities;

namespace Kernel.Reviews.Queries;
public class UserReviewListQueryHandler : IRequestHandler<UserReviewListQuery, IQueryable<UserReview>>
{
    private readonly IUserReviewRepository _userReviewRepository;
    public UserReviewListQueryHandler(IUserReviewRepository userReviewRepository)
    {
        _userReviewRepository = userReviewRepository;
    }

    public async Task<IQueryable<UserReview>> Handle(UserReviewListQuery request, CancellationToken cancellationToken)
    {
        return _userReviewRepository.GetAll().Where(x => x.ParentId == null);
    }
}