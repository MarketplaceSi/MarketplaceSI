using Domain.Entities;

namespace Kernel.Categories.Queries;
public class CategoriesListQueryHandler : IRequestHandler<CategoriesListQuery, IQueryable<Category>>
{
    private readonly IReadOnlyCategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriesListQueryHandler(IReadOnlyCategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<IQueryable<Category>> Handle(CategoriesListQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_categoryRepository.GetAll());
    }
}