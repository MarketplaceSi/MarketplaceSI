using AppAny.HotChocolate.FluentValidation;
using MarketplaceSI.Core.Kernel.Account.Commands;
using MarketplaceSI.Core.Kernel.Account;
using MediatR;
using HotChocolate.Subscriptions;
using Kernel.Accounts.Commands;
using MarketplaceSI.Graphql.Subscriptions;
using MarketplaceSI.Core.Dto.Generic;
using Kernel.Accounts.Commandsp;

namespace MarketplaceSI.Graphql.Mutations;
[ExtendObjectType(OperationTypeNames.Mutation)]
public class AccountMutations
{
    public async Task<AccountPayloadBase> RegisterUserAsync(
        [UseFluentValidation] AccountCreateCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
    public async Task<AccountTokenPayload> LoginAsync(
       [UseFluentValidation] AccountLoginCommand input,
       [Service] IMediator mediator,
       [Service] ITopicEventSender eventSender,
       CancellationToken cancellationToken)
    {
        var payload = await mediator.Send(input, cancellationToken);

        await eventSender.SendAsync(nameof(UserSubscription.SubscribeUser), payload.User);
        return payload;
    }
    public async Task<AccountTokenPayload> RenewTokenAsync(
       [UseFluentValidation] AccountRenewTokenCommand input,
       [Service] IMediator mediator,
       CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
    public async Task<ActionPayload> RequestResetPasswordAsync(
      [UseFluentValidation] AccountResetPasswordCommand input,
      [Service] IMediator mediator,
      CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
    public async Task<ActionPayload> RequestEmailVerifyAsync(
      [UseFluentValidation] AccountRequestEmailVerifyCommand input,
      [Service] IMediator mediator,
      CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
    public async Task<ActionPayload> SetPasswordAsync(
      [UseFluentValidation] AccountSetPasswordCommand input,
      [Service] IMediator mediator,
      CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    //[Authorize]
    public async Task<ActionPayload> DeleteAccountAsync(
      AccountDeleteCommand input,
      [Service] IMediator mediator,
      CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
}

