using Infrastructure.Extensions;
using MarketplaceSI.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Reviews.Commands;
public class ProductReviewRemoveCommandHandler : IRequestHandler<ProductReviewRemoveCommand, ActionPayload>
{
    private readonly IProductRepository _productRepository;
    private readonly IUserService _userService;
    private readonly IProductReviewRepository _productReviewRepository;
    public ProductReviewRemoveCommandHandler(IUserService userService, IProductRepository productRepository, IProductReviewRepository productReviewRepository)
    {
        _productRepository = productRepository;
        _userService = userService;
        _productReviewRepository = productReviewRepository;
    }


    public async Task<ActionPayload> Handle(ProductReviewRemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync();
        if (user == null)
        {
            throw new ApiException("user_not_exists");
        }
        var review = await _productReviewRepository.GetAll().Where(x => x.Id == request.Id).Include(x => x.Product).ThenInclude(p => p.Reviews).FirstOrDefaultAsync();
        if (review == null)
        {
            return new ActionPayload(true);
        }
        if (review.CreatedById != user.Id)
        {
            throw new ApiException("access_forbidden");
        }
        var product = review.Product;
        product.Reviews.Remove(review);
        product.ReviewsAmount = product.Reviews.Count;
        product.Rating = product.Reviews.GetRating();

        _productRepository.Update(product);
        await _productReviewRepository.DeleteAsync(review);
        await _productReviewRepository.SaveChangesAsync();
        return new ActionPayload(true);
    }
}