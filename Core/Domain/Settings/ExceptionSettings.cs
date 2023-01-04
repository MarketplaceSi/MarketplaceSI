namespace MarketplaceSI.Core.Domain.Settings;

public class ExceptionSettings
{
    public Dictionary<string, List<ExceptionItem>> Items { get; set; } = default!;
}

public class ExceptionItem
{
    public int Id { get; set; }
    public string Code { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;
}