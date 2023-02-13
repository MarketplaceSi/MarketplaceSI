using AppAny.HotChocolate.FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Users;
using Kernel.Users.Commands;
using MarketplaceSI.Core.Dto.Generic;
using MediatR;

namespace MarketplaceSI.Graphql.Mutations;
[ExtendObjectType(OperationTypeNames.Mutation)]
public class AddressMutation
{
    [Authorize]
    public async Task<AddressPayload> AddAddressAsync(
        [UseFluentValidation] UserAddressCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    [Authorize]
    public async Task<AddressPayload> UpdateAddressAsync(
        [UseFluentValidation] UserAddressUpdateCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }


    [Authorize]
    public async Task<ActionPayload> RemoveAddressAsync(
        UserAddressRemoveCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
}
