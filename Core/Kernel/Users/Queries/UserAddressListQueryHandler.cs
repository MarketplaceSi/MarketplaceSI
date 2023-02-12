namespace Kernel.Users.Queries;
public class UserAddressListQueryHandler : IRequestHandler<UserAddressListQuery, IQueryable<Address>>
{
    private readonly IUserService _userService;
    private readonly IAddressRepository _addressRepository;
    public UserAddressListQueryHandler(IUserService userService, IAddressRepository addressRepository)
    {
        _userService = userService;
        _addressRepository = addressRepository;
    }


    public async Task<IQueryable<Address>> Handle(UserAddressListQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }

        return _addressRepository.GetAll().Where(_ => _.UserId == user.Id);
    }
}