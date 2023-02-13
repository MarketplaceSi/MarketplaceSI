namespace Kernel.Reviews.Commands;
public record ProductReviewAddCommand(Guid? Id, Guid? ReviewId, string Comment) 
    : IRequest<ProductReviewPayload>;
