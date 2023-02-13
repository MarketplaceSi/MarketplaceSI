using Domain.Entities;

namespace MarketplaceSI.Graphql.ObjectTypes;
public class ProductType : ObjectType<Product>
{
    protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor.Field(x => x.Favorited)
            .IsProjected(true);
        descriptor
            .Field("favorited")
            .Resolve(context => {
                try
                {
                    var favorited = context.Parent<Product>()?.Favorited;
                    return favorited != null && favorited.Any();
                }
                catch (Exception ex)
                {
                    return false;
                }
            });

        descriptor
            //.Ignore(f => f.Favorited)
            .Ignore(f => f.IsDeleated);
    }
}