using Domain.Entities;

namespace Kernel.Products.Queries;
public record ProductCommand(Guid Id) : IRequest<Product>;