using FluentValidation;
using Kernel.Products.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Products;
public class ProductCreateCommandValidator : AbstractValidator<ProductCreateCommand>
{
    public ProductCreateCommandValidator()
    {
        RuleFor(_ => _.Title).NotEmpty().Length(3, 100);
        RuleFor(_ => _.Price).NotEmpty();
        RuleFor(_ => _.Description).NotEmpty().Length(3, 500);
        RuleFor(_ => _.Condition).NotNull().IsInEnum();
        RuleFor(_ => _.CategoryId).Id();
        RuleForEach(_ => _.Files)
            .ChildRules(i => i.RuleFor(f => f.Length)
            .NotNull()
            .MaxPictureSize());
    }
}