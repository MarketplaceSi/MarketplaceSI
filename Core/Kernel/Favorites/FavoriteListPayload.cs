using Domain.Entities;

namespace Kernel.Favorites;
public class FavoriteListPayload : Payload
{
    public FavoriteListPayload(IQueryable<Product> products)
    {
        Products = products;
    }
    public FavoriteListPayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }

    public IQueryable<Product>? Products { get; set; }
}