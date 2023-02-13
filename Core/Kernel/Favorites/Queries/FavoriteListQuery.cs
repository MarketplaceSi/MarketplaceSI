using Domain.Entities;

namespace Kernel.Favorites.Queries;
public record FavoriteListQuery() : IRequest<IQueryable<Product>>;