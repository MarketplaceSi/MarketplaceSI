namespace MarketplaceSI.Core.Kernel.Users;

public class UsersListPayload : Payload
{
    public UsersListPayload(IQueryable<User> users)
    {
        Users = users;
    }
    public UsersListPayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }
    public IQueryable<User>? Users { get; set; }
}
