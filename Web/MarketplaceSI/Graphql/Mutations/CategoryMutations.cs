using AppAny.HotChocolate.FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Categories;
using Kernel.Categories.Commands;
using MarketplaceSI.Core.Dto.Generic;
using MarketplaceSI.Graphql.GraphqlExtensions;
using MarketplaceSI.Graphql.InputTypes;
using MediatR;

namespace MarketplaceSI.Graphql.Mutations; [ExtendObjectType(OperationTypeNames.Mutation)]
public class CategoryMutations
{
    public async Task<CategoryPayloadBase> CategoryCreateAsync(
    [UseFluentValidation] CategoryCreateCommandInput input,
    [Service] IMediator mediator,
    CancellationToken cancellationToken)
    {
        var File = input.File != null ? input.File.TransformToFileData() : null;
        return await mediator.Send(new CategoryCreateCommand(File, input.Name, input.ParentId), cancellationToken);
    }

    public async Task<CategoryPayloadBase> CategoryUpdateAsync(
    [UseFluentValidation] CategoryUpdateCommandInput input,
    [Service] IMediator mediator,
    CancellationToken cancellationToken)
    {
        var File = input.File != null ? input.File.TransformToFileData() : null;
        return await mediator.Send(new CategoryUpdateCommand(File, input.Id, input.Name, input.ParentId, input.Active), cancellationToken);
    }
    [Authorize]
    public async Task<ActionPayload> CategoryRemoveAsync(
    [UseFluentValidation] CategoryRemoveCommand input,
    [Service] IMediator mediator,
    CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
    [Authorize]
    public async Task<CategoriesListPayload> CategoriesStatusChangeAsync(
    [UseFluentValidation] CategoriesStatusChangeCommand input,
    [Service] IMediator mediator,
    CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
}
