using FluentValidation;
using Kernel.Accounts.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Account
{
    public class AccountLoginCommandValidator : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginCommandValidator()
        {
            RuleFor(_ => _.Email).Email();
            RuleFor(_ => _.Password).Password();
        }
    }
}
