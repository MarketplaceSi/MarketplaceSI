﻿namespace MarketplaceSI.Web.Api.Graphql.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class UserQueries
{
    //[Authorize]
    //[UsePaging]
    //[UseFiltering]
    //[UseSorting]
    //// [UseFiltering(typeof(SessionFilterInputType))]
    //public async Task<IQueryable<User>?> UsersList(
    //    [Service] IMediator mediator,
    //    CancellationToken cancellationToken)
    //{
    //    return (await mediator.Send(new UsersListQuery(), cancellationToken));
    //}

    //public async Task<User> UserById(Guid? input,
    //    [Service] IMediator mediator,
    //    CancellationToken cancellationToken)
    //{
    //    return await mediator.Send(new UserQuery(input), cancellationToken);
    //}

    //[Authorize]
    //[UsePaging]
    //public async Task<IQueryable<Address>?> AddressesListAsync(
    //    [Service] IMediator mediator,
    //    CancellationToken cancellationToken)
    //{
    //    return await mediator.Send(new UserAddressListQuery(), cancellationToken);
    //}
}
