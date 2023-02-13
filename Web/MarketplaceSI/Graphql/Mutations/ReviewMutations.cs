using AppAny.HotChocolate.FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Reviews;
using Kernel.Reviews.Commands;
using MarketplaceSI.Core.Dto.Generic;
using MediatR;

namespace MarketplaceSI.Graphql.Mutations;
[ExtendObjectType(OperationTypeNames.Mutation)]
public class ReviewMutations
{
    [Authorize]
    public async Task<ProductReviewPayload> ProductReviewAddAsync(
        [UseFluentValidation] ProductReviewAddCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    [Authorize]
    public async Task<ActionPayload> ProductReviewRemoveAsync(
        ProductReviewRemoveCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }


    [Authorize]
    public async Task<UserReviewPayload> UserReviewAddAsync(
        [UseFluentValidation] UserReviewAddCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

}