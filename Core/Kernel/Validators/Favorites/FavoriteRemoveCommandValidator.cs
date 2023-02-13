using FluentValidation;
using Kernel.Favorites.Commands;
using MarketplaceSI.Core.Kernel.Validators;

namespace Kernel.Validators.Favorites;
public class FavoriteRemoveCommandValidator : AbstractValidator<FavoriteRemoveCommand>
{
    public FavoriteRemoveCommandValidator()
    {
        RuleFor(_ => _.Id).Id();
    }
}