using Domain.Entities;
using Kernel.Products.Queries;
using MediatR;

namespace MarketplaceSI.Graphql.Queries;
[ExtendObjectType(OperationTypeNames.Query)]
public class ProductQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<Product>?> ProductsList(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return (await mediator.Send(new ProductListQuery(), cancellationToken));
    }

    [UseProjection]
    public async Task<Product> ProductById(
        ProductCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
}