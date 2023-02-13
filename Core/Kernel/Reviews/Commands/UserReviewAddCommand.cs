namespace Kernel.Reviews.Commands;
public record UserReviewAddCommand(Guid? Id, Guid? ReviewId, string Comment, int? Stars) 
    : IRequest<UserReviewPayload>;