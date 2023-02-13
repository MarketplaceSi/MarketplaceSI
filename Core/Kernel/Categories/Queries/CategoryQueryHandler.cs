using Domain.Entities;
using Domain.Repositories.DataLoaders;

namespace Kernel.Categories.Queries;
public class CategoryQueryHandler : IRequestHandler<CategoryQuery, Category>
{
    private readonly ICategoryByIdDataLoader _categoryByIdDataLoader;

    public CategoryQueryHandler(ICategoryByIdDataLoader categoryByIdDataLoader)
    {
        _categoryByIdDataLoader = categoryByIdDataLoader;
    }


    public async Task<Category> Handle(CategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryByIdDataLoader.LoadAsync(request.Id);

        if (category == null)
        {
            throw new ApiException("category_not_exists");
        }

        return category;
    }
}