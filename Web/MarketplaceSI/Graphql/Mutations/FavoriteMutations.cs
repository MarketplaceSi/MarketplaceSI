using AppAny.HotChocolate.FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Favorites.Commands;
using Kernel.Products;
using MarketplaceSI.Core.Dto.Generic;
using MediatR;

namespace MarketplaceSI.Graphql.Mutations;
[ExtendObjectType(OperationTypeNames.Mutation)]
public class FavoriteMutations
{
    [Authorize]
    public async Task<ProductPayloadBase> FavoriteAddAsync(
        [UseFluentValidation] FavoriteAddCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    [Authorize]
    public async Task<ProductPayloadBase> FavoriteRemoveAsync(
        [UseFluentValidation] FavoriteRemoveCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    [Authorize]
    public async Task<ActionPayload> FavoriteClearAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new FavoriteClearCommand(), cancellationToken);
    }
}
