using Infrastructure.Extensions;

namespace Kernel.Categories.Commands;
public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand, ActionPayload>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStorageService _storageService;
    public CategoryRemoveCommandHandler(ICategoryRepository categoryRepository, IStorageService storageService)
    {
        _categoryRepository = categoryRepository;
        _storageService = storageService;
    }
    public async Task<ActionPayload> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
        {
            return new ActionPayload(true);
        }
        await _storageService.DeleteObjectAsync(category.Picture.GetFileName(), category.GetPath());
        //TODO:Check for products. Remove Products? Dont allow to remove the category if any product?
        await _categoryRepository.DeleteAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return new ActionPayload(true);
    }
}