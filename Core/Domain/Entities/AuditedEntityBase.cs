using MarketplaceSI.Core.Domain.Entities.Interfaces;

namespace Domain.Entities;
public class AuditedEntityBase : IAuditedEntityBase
{
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = default;
    public string LastModifiedBy { get; set; } = string.Empty;
    public DateTime LastModifiedAt { get; set; } = default;
}