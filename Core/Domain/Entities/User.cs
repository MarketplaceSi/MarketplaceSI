using MarketplaceSI.Core.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceSI.Core.Domain.Entities;

public class User : IdentityUser<Guid>, IAuditedEntityBase
{
    [StringLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [StringLength(50)]
    public string LastName { get; set; } = String.Empty;

    public string Avatar { get; set; } = String.Empty;

    public string CreatedBy { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = default;
    public string LastModifiedBy { get; set; } = String.Empty;
    public DateTime LastModifiedAt { get; set; } = default;

    [StringLength(6, MinimumLength = 2)]
    public string LangKey { get; set; } = String.Empty;

    [StringLength(20)]
    [JsonIgnore]
    public string? ActivationKey { get; set; } = String.Empty;

    [StringLength(20)]
    [JsonIgnore]
    public string? ResetKey { get; set; } = String.Empty;

    public DateTime? ResetDate { get; set; }

    [Required]
    public bool Activated { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }

    public DateTime? DateOfBirth { get; set; } = default;
    public string Description { get; set; } = string.Empty;
    public bool IsDeleated { get; set; }
    public string DealitedReason { get; set; } = string.Empty;
    public DateTime? DealetedAt { get; set; } = default!;
    public virtual ICollection<Address> Addresses { get; set; } = default!;

    public override string ToString()
    {
        return "User{" +
               $"ID='{Id}'" +
               $", Login='{UserName}'" +
               $", FirstName='{FirstName}'" +
               $", LastName='{LastName}'" +
               $", Email='{Email}'" +
               $", Avatar='{Avatar}'" +
               $", Activated='{Activated}'" +
               $", LangKey='{LangKey}'" +
               $", ActivationKey='{ActivationKey}'" +
               "}";
    }

}