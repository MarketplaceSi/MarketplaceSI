using FluentValidation;
using Kernel.Reviews.Commands;

namespace Kernel.Validators.Reviews;
public class UserReviewAddCommandValidator : AbstractValidator<UserReviewAddCommand>
{
    public UserReviewAddCommandValidator()
    {
        RuleFor(_ => _.Comment)
            .NotEmpty()
            .MinimumLength(3);
        When(_ => _.Id != null, () => {
            RuleFor(_ => _.Stars)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);
        });

        When(_ => _.Id == null, () => {
            RuleFor(_ => _.ReviewId)
                .NotNull();
            RuleFor(_ => _.Stars)
                .Null();
        });
    }
}