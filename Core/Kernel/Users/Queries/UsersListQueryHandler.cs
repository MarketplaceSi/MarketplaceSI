using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Users.Queries;

public class UsersListQueryHandler : IRequestHandler<UsersListQuery, IQueryable<User>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UsersListQueryHandler(UserManager<User> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IQueryable<User>> Handle(UsersListQuery request, CancellationToken cancellationToken)
    {
        return _userManager.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).Where(x => x.IsDeleated == false).Include(u => u.Addresses).AsQueryable();
    }
}