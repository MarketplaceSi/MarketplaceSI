namespace MarketplaceSI.Core.Domain.Settings;

public class CorsSettings
{
    public string AllowedOrigins { get; set; } = string.Empty;
    public string AllowedMethods { get; set; } = string.Empty;
    public string AllowedHeaders { get; set; } = string.Empty;
    public string ExposedHeaders { get; set; } = string.Empty;
    public bool AllowCredentials { get; set; }
    public int MaxAge { get; set; }
}