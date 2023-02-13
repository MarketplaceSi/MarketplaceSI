using FluentValidation;

namespace MarketplaceSI.Graphql.InputTypes; 
public class CategoryCreateCommandInputValidator : AbstractValidator<CategoryCreateCommandInput>
{
    public CategoryCreateCommandInputValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
}