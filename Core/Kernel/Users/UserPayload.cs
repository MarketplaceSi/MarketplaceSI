namespace MarketplaceSI.Core.Kernel.Users;

public class UserPayload : Payload
{
    public UserPayload(User user)
    {
        User = user;
    }

    public User? User { get; set; }
}
