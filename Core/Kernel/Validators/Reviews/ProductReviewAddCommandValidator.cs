using FluentValidation;
using Kernel.Reviews.Commands;

namespace Kernel.Validators.Reviews;
public class ProductReviewAddCommandValidator : AbstractValidator<ProductReviewAddCommand>
{
    public ProductReviewAddCommandValidator()
    {
        RuleFor(_ => _.Comment)
            .NotEmpty()
            .MinimumLength(3);

        When(_ => _.Id == null, () => {
            RuleFor(_ => _.ReviewId)
                .NotNull();
        });


    }
}