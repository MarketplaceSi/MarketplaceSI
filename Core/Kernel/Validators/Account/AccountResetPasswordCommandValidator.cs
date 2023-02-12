using FluentValidation;
using Kernel.Accounts.Commandsp;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Account;
public class AccountResetPasswordCommandValidator : AbstractValidator<AccountResetPasswordCommand>
{
    public AccountResetPasswordCommandValidator()
    {
        RuleFor(_ => _.Email)
            .Email();
    }
}