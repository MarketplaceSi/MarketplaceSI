using Domain.Entities;

namespace Kernel.Reviews.Queries;
public class ProductReviewListQueryHandler : IRequestHandler<ProductReviewListQuery, IQueryable<ProductReview>>
{
    private readonly IReadOnlyProductReviewRepository _reviewRepository;

    public ProductReviewListQueryHandler(IReadOnlyProductReviewRepository productReviewRepository)
    {
        _reviewRepository = productReviewRepository;
    }


    public async Task<IQueryable<ProductReview>> Handle(ProductReviewListQuery request, CancellationToken cancellationToken)
    {
        return _reviewRepository.GetAll().Where(x => x.ProductId != null);
    }
}