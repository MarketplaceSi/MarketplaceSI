using MarketplaceSI.Core.Domain.Entities;

namespace Domain.Entities;
public class Favorite
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}