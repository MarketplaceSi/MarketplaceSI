namespace MarketplaceSI.Graphql.InputTypes;
public record CategoryCreateCommandInput(IFile? File, string Name, Guid? ParentId);
public class CategoryCreateCommandType : InputObjectType<CategoryCreateCommandInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CategoryCreateCommandInput> descriptor)
    {
        descriptor.Field(f => f.File).Type<UploadType>();
        base.Configure(descriptor);
    }
}