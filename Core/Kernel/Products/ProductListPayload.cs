using Domain.Entities;

namespace Kernel.Products;
public class ProductListPayload : Payload
{
    public ProductListPayload(IQueryable<Product> products)
    {
        Products = products;
    }
    public ProductListPayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }
    public IQueryable<Product>? Products { get; set; }
}