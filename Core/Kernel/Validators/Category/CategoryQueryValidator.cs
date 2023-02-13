using FluentValidation;
using Kernel.Categories.Queries;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Category;
public class CategoryQueryValidator : AbstractValidator<CategoryQuery>
{
    public CategoryQueryValidator()
    {
        RuleFor(_ => _.Id).Id();
    }
}