using Domain.Entities;

namespace Kernel.Reviews;
public class UserReviewPayload : Payload
{
    public UserReviewPayload(UserReview review)
    {
        UserReview = review;
    }

    public UserReview? UserReview { get; set; }

}