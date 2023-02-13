using Domain.Entities;
using GreenDonut;

namespace Domain.Repositories.DataLoaders;
public interface IReviewByIdDataLoader : IDataLoader<Guid, UserReview>
{
}