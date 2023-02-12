using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Dto.Enums;

namespace Domain.Entities
{
    public class Product : EntityBase<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public List<string> Pictures { get; set; } = new List<string>();
        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Guid CategoryId { get; set; }
        //public Category Category { get; set; } = default!;
        public ProductStatus Status { get; set; }
        public ProductCondition Condition { get; set; }
        public bool IsDeleated { get; set; } = false;
        public double? Rating { get; set; }
        public int ReviewsAmount { get; set; } = default;
        //public virtual ICollection<Favorite> Favorited { get; set; } = default!;
        //public virtual ICollection<ProductReview> Reviews { get; set; } = default!;
        public int ViewedCount { get; set; } = 0;
        public string GetPath()
        {
            return $"{CategoryId}/{OwnerId}/{Id}/";
        }
    }
}
