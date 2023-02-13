using Domain.Entities;
using Kernel.Reviews.Queries;
using MediatR;

namespace MarketplaceSI.Graphql.Queries;
[ExtendObjectType(OperationTypeNames.Query)]
public class ReviewQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<ProductReview>?> ProductReviewsList(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return (await mediator.Send(new ProductReviewListQuery(), cancellationToken));
    }
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<UserReview>> UserReviewsList(
    [Service] IMediator mediator,
    CancellationToken cancellationToken)
    {
        return await mediator.Send(new UserReviewListQuery(), cancellationToken);
    }
}