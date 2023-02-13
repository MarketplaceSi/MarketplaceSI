using Domain.Entities;
using MarketplaceSI.Domain.Repositories.Interfaces;

namespace Kernel.Products.Queries;
public class ProductListQueryHandler : IRequestHandler<ProductListQuery, IQueryable<Product>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyProductRepository _productRepository;
    private readonly IUserService _userService;
    public ProductListQueryHandler(IReadOnlyProductRepository productRepository, IUserService userService, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<IQueryable<Product>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        var userId = user?.Id ?? new Guid();

        return _productRepository.GetAll(user?.Id);
    }
}
