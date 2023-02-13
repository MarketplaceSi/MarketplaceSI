using MarketplaceSI.Core.Domain.Entities;

namespace Domain.Entities;
public class UserReview : EntityBase<Guid>
{
    public Guid CreatedById { get; set; }
    public virtual User CreatedBy { get; set; } = default!;
    public string Comment { get; set; } = string.Empty;
    public int? Stars { get; set; }
    public Guid? ParentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual UserReview? Parent { get; set; }
    public virtual ICollection<UserReview> Replays { get; set; } = default!;
}