using MarketplaceSI.Core.Domain.Security.Interfaces;

namespace Kernel.Categories.Commands;
public class CategoriesStatusChangeCommandHandler : IRequestHandler<CategoriesStatusChangeCommand, CategoriesListPayload>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserAccessor _userAccessor;
    public CategoriesStatusChangeCommandHandler(ICategoryRepository categoryRepository, IUserAccessor userAccessor)
    {
        _categoryRepository = categoryRepository;
        _userAccessor = userAccessor;
    }

    public async Task<CategoriesListPayload> Handle(CategoriesStatusChangeCommand request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAll().Where(c => request.CategoriesId.Contains(c.Id));
        foreach (var category in categories)
        {
            category.Active = request.Active;
        }
        _categoryRepository.UpdateRange(categories);
        await _categoryRepository.SaveChangesAsync();
        return new CategoriesListPayload(categories);
    }
}
