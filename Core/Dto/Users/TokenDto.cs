namespace MarketplaceSI.Core.Dto.Users;

public class TokenDto
{
    public TokenDto(string token, string refreshToken, DateTime refreshTokenExpiry)
    {
        Token = token;
        RefreshToken = refreshToken;
        RefreshTokenExpiry = refreshTokenExpiry;
    }

    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiry { get; set; } = default;
}
