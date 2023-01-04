namespace MarketplaceSI.Core.Domain.Settings
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public Dictionary<string, string> Templates { get; set; } = new Dictionary<string, string>();
    }
}
