using Domain.Entities;

namespace MarketplaceSI.Graphql.ObjectTypes;
public class CategoryType : ObjectType<Category>
{
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor.Field(x => x.Categories)
            .Resolve(context =>
            {
                return context.Parent<Category>()?.Categories;
            })
            .IsProjected(true);
    }
}