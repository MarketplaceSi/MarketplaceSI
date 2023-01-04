using Microsoft.AspNetCore.Identity;

namespace MarketplaceSI.Core.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}