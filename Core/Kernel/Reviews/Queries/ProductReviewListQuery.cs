using Domain.Entities;

namespace Kernel.Reviews.Queries;
public record ProductReviewListQuery() : IRequest<IQueryable<ProductReview>>;