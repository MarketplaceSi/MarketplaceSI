using Domain.Entities;

namespace Kernel.Categories.Commands;
public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, CategoryPayloadBase>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;
    public CategoryCreateCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IStorageService storageService)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _storageService = storageService;
    }
    public async Task<CategoryPayloadBase> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId != null && await _categoryRepository.GetByIdAsync(request.ParentId.Value) == null)
        {
            throw new ApiException("category_parent_not_exists", System.Net.HttpStatusCode.BadRequest);
        }
        if (await _categoryRepository.Exists(c => c.Name.Equals(request.Name)))
        {
            throw new ApiException("category_name_allready_exists", System.Net.HttpStatusCode.BadRequest);
        }
        var category = _categoryRepository.Add(new Category()
        {
            Name = request.Name,
            CategoryId = request.ParentId
        });
        category.Picture = await _storageService.UploadImageAsync(request.File.FileStream, "logo", category.GetPath());
        await _categoryRepository.SaveChangesAsync();
        return new CategoryPayloadBase(category);
    }
}
