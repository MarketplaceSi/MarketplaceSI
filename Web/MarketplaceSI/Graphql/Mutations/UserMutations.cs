using AppAny.HotChocolate.FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Users.Commands;
using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Graphql.GraphqlExtensions;
using MarketplaceSI.Graphql.InputTypes;
using MediatR;

namespace MarketplaceSI.Graphql.Mutations;
[ExtendObjectType(OperationTypeNames.Mutation)]
public class UserMutations
{
    [Authorize]
    public async Task<User> EditUserAsync(
     [UseFluentValidation] UserEditCommandInput input,
     [Service] IMediator mediator,
     CancellationToken cancellationToken)
    {
        var file = input.File != null ? input.File.TransformToFileData() : null;
        return await mediator.Send(new AccountEditCommand(file, input.FirstName, input.LastName, input.Description, input.DateOfBirth, input.PhoneNumber), cancellationToken);
    }

}