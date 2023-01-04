namespace MarketplaceSI.Core.Domain.Entities.Interfaces;

public interface IAuditedEntityBase
{
    string CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string LastModifiedBy { get; set; }
    DateTime LastModifiedAt { get; set; }
}