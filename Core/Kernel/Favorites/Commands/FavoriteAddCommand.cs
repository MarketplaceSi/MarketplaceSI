using Kernel.Products;

namespace Kernel.Favorites.Commands;
public record FavoriteAddCommand(Guid Id) : IRequest<ProductPayloadBase>;