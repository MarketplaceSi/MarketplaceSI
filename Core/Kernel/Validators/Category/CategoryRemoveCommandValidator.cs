using FluentValidation;
using Kernel.Categories.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Category;
public class CategoryRemoveCommandValidator : AbstractValidator<CategoryRemoveCommand>
{
    public CategoryRemoveCommandValidator()
    {
        RuleFor(_ => _.Id)
           .Id();
    }
}