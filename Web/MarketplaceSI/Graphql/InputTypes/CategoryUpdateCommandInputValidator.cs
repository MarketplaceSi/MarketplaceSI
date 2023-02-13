using FluentValidation;

namespace MarketplaceSI.Graphql.InputTypes;
public class CategoryUpdateCommandInputValidator : AbstractValidator<CategoryUpdateCommandInput>
{
    public CategoryUpdateCommandInputValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty();
    }
}