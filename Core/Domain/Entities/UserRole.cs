using Microsoft.AspNetCore.Identity;

namespace MarketplaceSI.Core.Domain.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}