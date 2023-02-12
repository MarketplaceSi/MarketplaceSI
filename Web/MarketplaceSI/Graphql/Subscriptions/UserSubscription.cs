using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Kernel.Users;

namespace MarketplaceSI.Graphql.Subscriptions
{
    public class UserSubscription
    {
        [Topic]
        [Subscribe]
        public UserPayload SubscribeUser([EventMessage] User user)
        {
            return new UserPayload(user);
        }
    }
}
