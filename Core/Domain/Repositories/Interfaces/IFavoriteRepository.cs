using Domain.Entities;
using System.Linq.Expressions;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;
public interface IFavoriteRepository
{
    Task<bool> Exists(Expression<Func<Favorite, bool>> predicate);
    Favorite Add(Favorite favorite);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
