namespace Kernel.Users.Queries;
    public record UserQuery(Guid? Id) : IRequest<User>;
