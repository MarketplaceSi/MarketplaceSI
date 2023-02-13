using FluentValidation;

namespace MarketplaceSI.Graphql.InputTypes;
public class ProductCreateCommandInputValidator : AbstractValidator<ProductCreateCommandInput>
{
    public ProductCreateCommandInputValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty();
        RuleFor(p => p.Description)
            .NotEmpty();
        RuleFor(p => p.Price)
            .GreaterThan(0);
        RuleFor(p => p.CategoryId)
            .NotNull()
            .NotEmpty();
        RuleFor(p => p.Condition)
            .NotNull()
            .IsInEnum();
    }
}