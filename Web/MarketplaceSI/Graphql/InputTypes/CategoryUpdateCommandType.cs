namespace MarketplaceSI.Graphql.InputTypes;
public record CategoryUpdateCommandInput(IFile? File, Guid Id, string Name, Guid? ParentId, bool Active);
public class CategoryUpdateCommandType : InputObjectType<CategoryUpdateCommandInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CategoryUpdateCommandInput> descriptor)
    {
        descriptor.Field(f => f.File).Type<UploadType>();
        base.Configure(descriptor);
    }
}