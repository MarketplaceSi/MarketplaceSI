namespace Kernel.Users.Commands;
public record UserAddressUpdateCommand(Guid? Id, string Name, string AddressLine, bool IsDefault) : IRequest<AddressPayload>;