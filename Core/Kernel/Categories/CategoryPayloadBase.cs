using Domain.Entities;

namespace Kernel.Categories;
public class CategoryPayloadBase : Payload
{
    public CategoryPayloadBase(Category category)
    {
        Category = category;
    }
    public CategoryPayloadBase(IReadOnlyList<Error> errors)
        : base(errors)
    {

    }
    public Category? Category { get; set; }
}
