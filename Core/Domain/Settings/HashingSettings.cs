namespace MarketplaceSI.Core.Domain.Settings;

public class HashingSettings
{
    public int Iterations { get; set; } = 10000;
    public int SaltSize { get; set; } = 16;
    public int KeySize { get; set; } = 32;
}