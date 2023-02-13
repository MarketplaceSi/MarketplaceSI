using MarketplaceSI.Core.Dto.Enums;
namespace MarketplaceSI.Graphql.InputTypes;
public record ProductCreateCommandInput(List<IFile> Files, string Title, string Description, double Price, Guid CategoryId, ProductCondition Condition);

public class ProductCreateCommandType : InputObjectType<ProductCreateCommandInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<ProductCreateCommandInput> descriptor)
    {
        descriptor.Field(f => f.Files).Type<ListType<UploadType>>();
        base.Configure(descriptor);
    }
}