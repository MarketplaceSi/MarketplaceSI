using FluentValidation;
using Kernel.Users.Commands;

namespace Kernel.Validators.User;
public class UserAddressCommandValidator : AbstractValidator<UserAddressCommand>
{
    public UserAddressCommandValidator()
    {
        RuleFor(_ => _.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(_ => _.AddressLine)
            .NotEmpty();

    }
}