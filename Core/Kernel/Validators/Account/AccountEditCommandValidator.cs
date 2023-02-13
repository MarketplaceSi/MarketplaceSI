using FluentValidation;
using Kernel.Users.Commands;

namespace Kernel.Validators.Account;
public class AccountEditCommandValidator : AbstractValidator<AccountEditCommand>
{
    public AccountEditCommandValidator()
    {
        RuleFor(_ => _.FirstName).MaximumLength(50);
        RuleFor(_ => _.LastName).MaximumLength(50);
        RuleFor(_ => _.Description).MinimumLength(3).MaximumLength(400);
    }
}