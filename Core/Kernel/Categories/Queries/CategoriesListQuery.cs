using Domain.Entities;

namespace Kernel.Categories.Queries;
public record CategoriesListQuery() : IRequest<IQueryable<Category>>;