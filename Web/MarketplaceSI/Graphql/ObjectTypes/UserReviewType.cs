using Domain.Entities;

namespace MarketplaceSI.Graphql.ObjectTypes;
public class UserReviewType : ObjectType<UserReview>
{
    protected override void Configure(IObjectTypeDescriptor<UserReview> descriptor)
    {
        descriptor
            .Field(r => r.Replays)
            .Resolve(context => {
                return context.Parent<UserReview>()?.Replays;
            });
    }
}