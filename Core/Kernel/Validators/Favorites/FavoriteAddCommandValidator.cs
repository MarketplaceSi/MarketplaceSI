using FluentValidation;
using Kernel.Favorites.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Favorites;
public class FavoriteAddCommandValidator : AbstractValidator<FavoriteAddCommand>
{
    public FavoriteAddCommandValidator()
    {
        RuleFor(x => x.Id).Id();
    }
}