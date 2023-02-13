namespace Kernel.Products.Commands;
public record ProductRemoveCommand(Guid Id) : IRequest<ActionPayload>;