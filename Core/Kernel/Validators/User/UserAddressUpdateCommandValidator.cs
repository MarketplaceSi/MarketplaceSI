using FluentValidation;
using Kernel.Users.Commands;

namespace Kernel.Validators.User;
public class UserAddressUpdateCommandValidator : AbstractValidator<UserAddressUpdateCommand>
{
    public UserAddressUpdateCommandValidator()
    {
        RuleFor(_ => _.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(_ => _.AddressLine)
            .NotEmpty();

    }
}