using Domain.Entities;

namespace Kernel.Categories;
public class CategoriesListPayload : Payload
{
    public CategoriesListPayload(IQueryable<Category> categories)
    {
        Categories = categories;
    }
    public CategoriesListPayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }
    public IQueryable<Category>? Categories { get; set; }
}