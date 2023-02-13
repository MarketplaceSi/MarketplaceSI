using Domain.Entities;

namespace Kernel.Favorites;
public class FavoritePayload : Payload
{
    public FavoritePayload(Product product)
    {
        Product = product;
    }
    public FavoritePayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }

    public Product? Product { get; set; }
}