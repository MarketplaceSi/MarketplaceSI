using Domain.Entities;

namespace Kernel.Products;
public class ProductPayloadBase : Payload
{
    public ProductPayloadBase(Product product)
    {
        Product = product;
    }
    public ProductPayloadBase(IReadOnlyList<Error> errors)
        : base(errors)
    {

    }
    public Product? Product { get; set; }
}