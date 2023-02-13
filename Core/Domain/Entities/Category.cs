using MarketplaceSI.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Category : EntityBase<Guid>
{
    [StringLength(50)]
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public Guid? CategoryId { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = default!;
    public string GetPath()
    {
        return $"{Id}/";
    }
}