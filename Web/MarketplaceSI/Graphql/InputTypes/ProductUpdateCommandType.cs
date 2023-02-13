using MarketplaceSI.Core.Dto.Enums;

namespace MarketplaceSI.Graphql.InputTypes;
public record ProductUpdateCommandInput(List<IFile?>? FilesToUpload, List<string>? FilesToDelete, Guid Id, string Title, string Description, double Price, Guid CategoryId, ProductCondition Condition);
public class ProductUpdateCommandType : InputObjectType<ProductUpdateCommandInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<ProductUpdateCommandInput> descriptor)
    {
        descriptor.Field(f => f.FilesToUpload).Type<ListType<UploadType>?>();
        base.Configure(descriptor);
    }
}