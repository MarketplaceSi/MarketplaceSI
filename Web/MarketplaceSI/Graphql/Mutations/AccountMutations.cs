using AppAny.HotChocolate.FluentValidation;
using MarketplaceSI.Core.Kernel.Account.Commands;
using MarketplaceSI.Core.Kernel.Account;
using MediatR;

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
}

