using FluentValidation;
using Kernel.Accounts.Commands;

namespace Kernel.Validators.Account;
public class AccountRequestEmailVerifyCommandValidator : AbstractValidator<AccountRequestEmailVerifyCommand>
{
    public AccountRequestEmailVerifyCommandValidator()
    {
        RuleFor(_ => _.Key)
            .NotEmpty();
    }
}
