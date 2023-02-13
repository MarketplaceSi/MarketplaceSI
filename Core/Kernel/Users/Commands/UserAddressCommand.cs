namespace Kernel.Users.Commands;
public record UserAddressCommand(string AddressLine, string Name, bool IsDefault = false) : IRequest<AddressPayload>;