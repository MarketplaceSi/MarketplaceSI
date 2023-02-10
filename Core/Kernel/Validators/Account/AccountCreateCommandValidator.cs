using FluentValidation;
using MarketplaceSI.Core.Kernel.Account.Commands;

namespace MarketplaceSI.Core.Kernel.Validators.Account;

public class AccountCreateCommandValidator : AbstractValidator<AccountCreateCommand>
{
   public AccountCreateCommandValidator()
   {
        RuleFor(_ => _.Email).Email();
        RuleFor(_ => _.FirstName).MaximumLength(50);
        RuleFor(_ => _.LastName).MaximumLength(50);
   }
}