using FluentValidation;
using Kernel.Accounts.Commands;

namespace Kernel.Validators.Account;
public class AccountRenewTokenCommandValidator : AbstractValidator<AccountRenewTokenCommand>
{
    public AccountRenewTokenCommandValidator()
    {
        RuleFor(_ => _.AccessToken).NotEmpty();
        RuleFor(_ => _.RefreshToken).NotEmpty();
    }
}