using Domain.Entities;

namespace Kernel.Reviews;
public class ProductReviewPayload : Payload
{
    public ProductReviewPayload(ProductReview review)
    {
        Review = review;
    }

    public ProductReview? Review { get; }
}