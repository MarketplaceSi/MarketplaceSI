namespace Kernel.Users.Queries;
public record UserAddressListQuery() : IRequest<IQueryable<Address>>;
