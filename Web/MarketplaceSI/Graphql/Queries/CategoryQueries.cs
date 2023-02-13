using AppAny.HotChocolate.FluentValidation;
using Domain.Entities;
using Kernel.Categories.Queries;
using MediatR;

namespace MarketplaceSI.Graphql.Queries;
[ExtendObjectType(OperationTypeNames.Query)]
public class CategoryQueries
{
    [UsePaging]
    [UseFiltering]
    public async Task<IQueryable<Category>?> CategoriesList(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return (await mediator.Send(new CategoriesListQuery(), cancellationToken));
    }

    public async Task<Category> CategoryById(
        [UseFluentValidation] CategoryQuery input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
}