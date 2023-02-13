using Domain.Entities;

namespace Kernel.Products.Queries;
public record ProductListQuery() : IRequest<IQueryable<Product>>;