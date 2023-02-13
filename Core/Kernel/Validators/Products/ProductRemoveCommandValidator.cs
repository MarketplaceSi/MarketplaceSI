using FluentValidation;
using Kernel.Products.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Products;
public class ProductRemoveCommandValidator : AbstractValidator<ProductRemoveCommand>
{
    public ProductRemoveCommandValidator()
    {
        RuleFor(_ => _.Id).Id();
    }
}