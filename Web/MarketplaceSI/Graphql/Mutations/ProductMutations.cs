using AppAny.HotChocolate.FluentValidation;
using HotChocolate.AspNetCore.Authorization;
using Kernel.Products;
using Kernel.Products.Commands;
using MarketplaceSI.Core.Dto.Generic;
using MarketplaceSI.Graphql.GraphqlExtensions;
using MarketplaceSI.Graphql.InputTypes;
using MediatR;

namespace MarketplaceSI.Graphql.Mutations;
[ExtendObjectType(OperationTypeNames.Mutation)]
public class ProductMutations
{
    [Authorize]
    public async Task<ProductPayloadBase> ProductCreateAsync(
        [UseFluentValidation] ProductCreateCommandInput input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var files = input.Files.TransformToFilesData();
        return await mediator.Send(new ProductCreateCommand(files,
            input.Title, input.Description, input.Price, input.CategoryId, input.Condition), cancellationToken);
    }

    [Authorize]
    public async Task<ProductPayloadBase> ProductUpdateAsync(
        [UseFluentValidation] ProductUpdateCommandInput input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var filesToUpload = input.FilesToUpload.TransformToFilesData();
        var filesToDelete = input.FilesToDelete != null ? input.FilesToDelete : new List<string>();
        return await mediator.Send(new ProductUpdateCommand(filesToUpload, filesToDelete,
            input.Id, input.Title, input.Description, input.Price, input.CategoryId, input.Condition), cancellationToken);
    }

    [Authorize]
    public async Task<ActionPayload> ProductRemoveAsync(
        [UseFluentValidation] ProductRemoveCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    [Authorize]
    public async Task<ProductPayloadBase> ProductStatusUpdateAsync(
        [UseFluentValidation] ProductStatusUpdateCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

}