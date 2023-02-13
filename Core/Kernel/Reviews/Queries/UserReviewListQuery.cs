using Domain.Entities;

namespace Kernel.Reviews.Queries;
public record UserReviewListQuery() : IRequest<IQueryable<UserReview>>;