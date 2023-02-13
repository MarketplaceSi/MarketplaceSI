using Domain.Entities;

namespace Kernel.Categories.Queries;
public record CategoryQuery(Guid Id) : IRequest<Category>;