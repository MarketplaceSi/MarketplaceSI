using FluentValidation;
using MarketplaceSI.Core.Kernel.Validators;

namespace MarketplaceSI.Graphql.InputTypes;
public class ProductUpdateCommandInputValidator : AbstractValidator<ProductUpdateCommandInput>
{
    public ProductUpdateCommandInputValidator()
    {
        RuleFor(_ => _.Title).NotEmpty().Length(3, 100);
        RuleFor(_ => _.Price).NotEmpty();
        RuleFor(_ => _.Description).NotEmpty().Length(3, 500);
        RuleFor(_ => _.Condition).NotNull().IsInEnum();
        RuleFor(_ => _.CategoryId).Id();
    }
}