namespace Kernel.Users.Commands;
public record UserAddressRemoveCommand(Guid Id) : IRequest<ActionPayload>;
