using FluentValidation;
using Kernel.Accounts.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Account;
public class AccountSetPasswordCommandValidator : AbstractValidator<AccountSetPasswordCommand>
{
    public AccountSetPasswordCommandValidator()
    {
        RuleFor(_ => _.Password)
            .Password();
        RuleFor(_ => _.Key)
            .NotEmpty();
    }
}