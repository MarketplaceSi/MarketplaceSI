namespace MarketplaceSI.Core.Kernel.Account;

public class AccountTokenPayload : Payload
{
    public AccountTokenPayload(User user, string accesstToken, string refreshToken, DateTime refreshTokenExpiry)
    {
        User = user;
        AccessToken = accesstToken;
        RefreshToken = refreshToken;
        RefreshTokenExpiry = refreshTokenExpiry;
    }

    public AccountTokenPayload(IReadOnlyList<Error> errors) : base(errors)
    {
    }

    public User? User { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiry { get; set; }
}
