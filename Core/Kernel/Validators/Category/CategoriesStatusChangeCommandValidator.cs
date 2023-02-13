using FluentValidation;
using Kernel.Categories.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Category;
public class CategoriesStatusChangeCommandValidator : AbstractValidator<CategoriesStatusChangeCommand>
{
    public CategoriesStatusChangeCommandValidator()
    {
        RuleFor(c => c.CategoriesId)
            .NotNull();
        RuleForEach(c => c.CategoriesId).Id();

    }
}