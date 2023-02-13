using FluentValidation;
using Kernel.Products.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Products;
public class ProductUpdateCommandValidator : AbstractValidator<ProductUpdateCommand>
{
    public ProductUpdateCommandValidator()
    {
        When(f => f.FilesToUpload != null, () =>
        {
            RuleForEach(_ => _.FilesToUpload)
            .ChildRules(i => i.RuleFor(f => f.Length)
            .NotNull()
            .MaxPictureSize());
        });
        RuleFor(_ => _.Title).NotEmpty().Length(3, 100);
        RuleFor(_ => _.Price).NotEmpty();
        RuleFor(_ => _.Description).NotEmpty().Length(3, 500);
        RuleFor(_ => _.Condition).NotNull().IsInEnum();
        RuleFor(_ => _.CategoryId).Id();
    }
}