namespace MarketplaceSI.Graphql.InputTypes;
public record UserEditCommandInput(IFile? File, string? FirstName, string? LastName, string? Description, DateTime? DateOfBirth, string? PhoneNumber);
public class UserEditCommandType : InputObjectType<UserEditCommandInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<UserEditCommandInput> descriptor)
    {
        descriptor.Field(f => f.File).Type<UploadType>();
        base.Configure(descriptor);
    }
}