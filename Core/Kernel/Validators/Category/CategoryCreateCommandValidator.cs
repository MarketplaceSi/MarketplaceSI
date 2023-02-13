using FluentValidation;
using Kernel.Categories.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Category;
public class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
{
    public CategoryCreateCommandValidator()
    {
        When(c => c.File != null, () => {
            RuleFor(c => c.File.Length).MaxPictureSize();
        });
    }
}