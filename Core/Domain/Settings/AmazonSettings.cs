namespace Domain.Settings;
public class AmazonSettings
{
    public string KeyId { get; set; } = string.Empty;
    public string KeySecret { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string CloudFrontDomain { get; set; } = string.Empty;
    public string BacketName { get; set; } = string.Empty;
    public int ExpirationInDays { get; set; }
    public string CloudFrontStorageDistributor { get; set; } = string.Empty;

}
