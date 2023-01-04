namespace MarketplaceSI.Core.Domain.Constants;

public static partial class Constants
{
    public const string DefaultLangKey = "en";

    public static class CustomClaimTypes
    {
        public const string VerifyEmail = "email";
    }
    public static class JWTKey
    {
        public const string SigningKey = "SigningKey";
        public const string VerifyKey = "VerifyKey";
    }
}
