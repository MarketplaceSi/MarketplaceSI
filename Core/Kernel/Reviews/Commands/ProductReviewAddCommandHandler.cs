using Domain.Entities;
using MarketplaceSI.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Reviews.Commands;
public class ProductReviewAddCommandHandler : IRequestHandler<ProductReviewAddCommand, ProductReviewPayload>
{
    private readonly IUserService _userService;
    private readonly IProductReviewRepository _productReviewRepository;
    private readonly IProductRepository _productRepository;

    public ProductReviewAddCommandHandler(IUserService userService,
        IProductReviewRepository productReviewsRepository,
        IProductRepository productRepository)
    {
        _userService = userService;
        _productReviewRepository = productReviewsRepository;
        _productRepository = productRepository;
    }


    public async Task<ProductReviewPayload> Handle(ProductReviewAddCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("register_to_add");
        }

        var review = new ProductReview()
        {
            Comment = request.Comment,
            CreatedById = user.Id,
            CreatedAt = DateTime.UtcNow
        };

        if (request.Id != null)
        {
            var product = await _productRepository.GetAll().Where(x => x.Id == request.Id.Value).Include(r => r.Reviews.Where(p => p.ProductId != null)).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new ApiException("product_not_exists");
            }
            if (product.OwnerId == user.Id)
            {
                throw new ApiException("access_forbidden");
            }
            review.ProductId = request.Id;
            product.Reviews.Add(review);
            product.ReviewsAmount = product.Reviews.Count;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return new ProductReviewPayload(review);
        }
        var parentReview = _productReviewRepository.GetAll().Where(x => x.Id == request.ReviewId).FirstOrDefault();
        if (parentReview == null)
        {
            throw new ApiException("must_be_related");
        }
        else if (parentReview.ParentId != null)
        {
            throw new ApiException("already_is_child");
        }
        else
        {
            review.ParentId = request.ReviewId;
            review = _productReviewRepository.Add(review);
            await _productReviewRepository.SaveChangesAsync();
        }
        return new ProductReviewPayload(review);
    }
}
