

namespace MarketplaceSI.Core.Kernel.Users.Queries;

public record UserQuery(Guid? Id) : IRequest<User>;
