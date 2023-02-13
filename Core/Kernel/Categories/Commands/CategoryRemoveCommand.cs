namespace Kernel.Categories.Commands;
public record CategoryRemoveCommand(Guid Id) : IRequest<ActionPayload>;