using Kernel.Products;

namespace Kernel.Favorites.Commands;
public record FavoriteRemoveCommand(Guid Id) : IRequest<ProductPayloadBase>;
