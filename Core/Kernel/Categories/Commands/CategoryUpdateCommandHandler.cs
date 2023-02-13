namespace Kernel.Categories.Commands;
public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, CategoryPayloadBase>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;
    public CategoryUpdateCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IStorageService storageService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _storageService = storageService;
    }
    public async Task<CategoryPayloadBase> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
        {
            throw new ApiException("category_not_exists");
        }
        if (category.Name != request.Name && await _categoryRepository.Exists(c => c.Name.Equals(request.Name)))
        {
            throw new ApiException("category_name_allready_exists", System.Net.HttpStatusCode.BadRequest);
        }
        category.Name = request.Name;
        if (request.File?.FileStream != null)
        {
            await _storageService.InvalidateFileLink("logo", category.GetPath());
            category.Picture = await _storageService.UploadImageAsync(request.File.FileStream, "logo", category.GetPath());
        }
        if (request.ParentId != null && await _categoryRepository.GetByIdAsync(request.ParentId.Value) == null)
        {
            throw new ApiException("category_parent_not_exists");
        }
        category.CategoryId = request.ParentId;
        category.Active = request.Active;

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryPayloadBase(category);
    }
}
