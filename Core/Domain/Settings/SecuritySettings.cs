namespace MarketplaceSI.Core.Domain.Settings;


public class SecuritySettings
{
    public Authentication Authentication { get; set; } = new Authentication();
    public bool EnforceHttps { get; set; }
}

public class Authentication
{
    public Jwt Jwt { get; set; } = new Jwt();
}

public class Jwt
{
    public string Base64Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int TokenExpirationInSeconds { get; set; }
    public int VerifyTokenExpirationInSeconds { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
    public string Verify64Secret { get; set; } = string.Empty;
}



