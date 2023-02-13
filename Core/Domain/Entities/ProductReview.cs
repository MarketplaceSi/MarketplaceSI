using MarketplaceSI.Core.Domain.Entities;

namespace Domain.Entities;
public class ProductReview : EntityBase<Guid>
{
    public Guid CreatedById { get; set; }
    public virtual User CreatedBy { get; set; } = default!;
    public string Comment { get; set; } = string.Empty;
    public int? Stars { get; set; }
    public Guid? ParentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual ProductReview? Parent { get; set; }
    public virtual ICollection<ProductReview> Replays { get; set; } = default!;
}
