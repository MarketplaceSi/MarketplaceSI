using FluentValidation;
using Kernel.Categories.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Category;
public class CategoryUpdateCommandValidator : AbstractValidator<CategoryUpdateCommand>
{
    public CategoryUpdateCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .MaximumLength(50);
        When(c => c.File != null, () => {
            RuleFor(c => c.File.Length).MaxPictureSize();
        });
    }
}
