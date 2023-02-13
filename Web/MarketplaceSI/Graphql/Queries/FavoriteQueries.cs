using Domain.Entities;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Favorites.Queries;
using MediatR;

namespace MarketplaceSI.Graphql.Queries;
[ExtendObjectType(OperationTypeNames.Query)]
public class FavoriteQueries
{
    [Authorize]
    [UsePaging]
    [UseFiltering]
    public async Task<IQueryable<Product>?> FavoritesList(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new FavoriteListQuery(), cancellationToken);
    }
}