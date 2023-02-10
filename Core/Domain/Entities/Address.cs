namespace MarketplaceSI.Core.Domain.Entities;

public class Address : EntityBase<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;
    public Guid UserId { get; set; }
}