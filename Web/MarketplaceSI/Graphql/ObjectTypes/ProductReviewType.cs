using Domain.Entities;

namespace MarketplaceSI.Graphql.ObjectTypes;
public class ProductReviewType : ObjectType<ProductReview>
{
    protected override void Configure(IObjectTypeDescriptor<ProductReview> descriptor)
    {
        descriptor
            .Ignore(f => f.ParentId)
            .Ignore(f => f.Parent)
            .Ignore(f => f.ProductId)
            .Ignore(f => f.Product);
    }
}