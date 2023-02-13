namespace Kernel.Reviews.Commands;
public record ProductReviewRemoveCommand(Guid Id) : IRequest<ActionPayload>;