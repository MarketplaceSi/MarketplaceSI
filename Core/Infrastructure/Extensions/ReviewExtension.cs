using Domain.Entities;

namespace Infrastructure.Extensions;
public static class ReviewExtension
{
    public static double? GetRating(this ICollection<ProductReview> reviews)
    {
        if (reviews.Count > 0)
        {
            return Math.Round((double)reviews.Sum(x => x.Stars) / (double)reviews.Count, 1);
        }
        else
        {
            return null;
        }
    }

    public static double? GetRating(this ICollection<UserReview> reviews)
    {
        if (reviews.Count > 0)
        {
            return Math.Round((double)reviews.Sum(x => x.Stars) / (double)reviews.Count, 1);
        }
        else
        {
            return null;
        }
    }
}