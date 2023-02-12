using FluentValidation;

namespace MarketplaceSI.Graphql.InputTypes;
public class UserEditCommandInputValidator : AbstractValidator<UserEditCommandInput>
{
    public UserEditCommandInputValidator()
    {
        When(u => !string.IsNullOrEmpty(u.FirstName), () =>
        {
            RuleFor(_ => _.FirstName)
            .MaximumLength(50);
        });

        When(u => !string.IsNullOrEmpty(u.LastName), () =>
        {
            RuleFor(_ => _.LastName)
            .MaximumLength(50);
        });
        When(u => !string.IsNullOrEmpty(u.Description), () =>
        {
            RuleFor(_ => _.Description)
            .MinimumLength(3)
            .MaximumLength(400);
        });

    }
}