using FluentValidation;
using Kernel.Products.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Products;
public class ProductStatusUpdateCommandValidator : AbstractValidator<ProductStatusUpdateCommand>
{
    public ProductStatusUpdateCommandValidator()
    {
        RuleFor(x => x.Id).Id();
        RuleFor(x => x.Status).NotEmpty().WithMessage("product_cant_be_draft").IsInEnum();
    }
}