using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Reviews.Commands;
public class UserReviewAddCommandHandler : IRequestHandler<UserReviewAddCommand, UserReviewPayload>
{
    private readonly IUserService _userService;
    private readonly IUserReviewRepository _userReviewRepository;
    public UserReviewAddCommandHandler(IUserService userService, IUserReviewRepository userReviewRepository)
    {
        _userReviewRepository = userReviewRepository;
        _userService = userService;
    }

    public async Task<UserReviewPayload> Handle(UserReviewAddCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }
        if (user.Id == request.Id)
        {
            throw new ApiException("same_user");
        }
        var review = new UserReview()
        {
            Comment = request.Comment,
            CreatedById = user.Id,
            CreatedAt = DateTime.UtcNow
        };

        if (request.Id != null)
        {
            var reviwedUser = await _userService.GetUserById(request.Id.Value);
            if (reviwedUser == null)
            {
                throw new ApiException("user_not_exists");
            }
            if (await _userReviewRepository.Exists(x => x.CreatedById == user.Id && x.UserId == reviwedUser.Id))
            {
                throw new ApiException("review_already_exist");
            }
            var reviews = await _userReviewRepository.GetAll().Where(x => x.Id == reviwedUser.Id).ToListAsync();
            review.Stars = request.Stars;
            review.UserId = request.Id;

            reviews.Add(review);
            reviwedUser.ReviewsAmount = reviews.Count;
            reviwedUser.Rating = reviews.GetRating();
            _userReviewRepository.Add(review);
            await _userReviewRepository.SaveChangesAsync();
            await _userService.UpdateUser(reviwedUser);
            return new UserReviewPayload(review);
        }
        else if (await _userReviewRepository.Exists(x => x.Id == request.ReviewId && x.UserId == user.Id))
        {
            var parentReview = await _userReviewRepository.GetByIdAsync(request.ReviewId.Value);
            if (parentReview == null)
            {
                throw new ApiException("review_not_exist");
            }
            if (parentReview.ParentId != null)
            {
                throw new ApiException("answer_already_given");
            }
            review.ParentId = request.ReviewId;
            review.UserId = parentReview.CreatedById;
        }
        else
        {
            throw new ApiException("must_be_related");
        }
        review = _userReviewRepository.Add(review);
        await _userReviewRepository.SaveChangesAsync();
        return new UserReviewPayload(review);
    }
}