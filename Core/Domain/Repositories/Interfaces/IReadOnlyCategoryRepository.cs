using Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Core.Domain.Repositories.Interfaces;
public interface IReadOnlyCategoryRepository : IReadOnlyBaseRepository<Category, Guid>
{
}