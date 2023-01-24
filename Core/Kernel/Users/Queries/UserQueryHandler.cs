using Domain.Repositories.DataLoaders;
using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Kernel.Users.Queries
{
    public class UserQueryHandler : IRequestHandler<UserQuery, User>
    {
        private readonly IUserByIdDataLoader _userLoader;
        private readonly IHttpContextAccessor _context;
        public UserQueryHandler(IUserByIdDataLoader userLoader, IHttpContextAccessor contextAccessor)
        {
            _userLoader = userLoader;
            _context = contextAccessor ?? throw new ArgumentNullException();
        }

        public async Task<User> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            if (id == null)
            {
                var userId = _context?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
                id = string.IsNullOrEmpty(userId) ? throw new ApiException("user_not_exists") : new Guid(userId);
            }
            var user = await _userLoader.LoadAsync(id ?? new Guid(), cancellationToken);
            if (user == null || user.IsDeleated)
            {
                throw new ApiException("user_not_exists");
            }
            return user;
        }
    }
}
