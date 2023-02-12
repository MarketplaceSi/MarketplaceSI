namespace Kernel.Users.Queries;

public record UsersListQuery() : IRequest<IQueryable<User>>;
