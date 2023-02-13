namespace Kernel.Users.Commands;
public class UserAddressCommandHandler : IRequestHandler<UserAddressCommand, AddressPayload>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IReadOnlyAddressRepository _readOnlyaddressRepository;
    private readonly IUserService _userService;
    public UserAddressCommandHandler(IAddressRepository addressRepository, IReadOnlyAddressRepository readOnlyAddressRepository, IUserService userService)
    {
        _addressRepository = addressRepository;
        _userService = userService;
        _readOnlyaddressRepository = readOnlyAddressRepository;
    }


    public async Task<AddressPayload> Handle(UserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();

        if (user == null)
        {
            throw new ApiException("invalid_token");
        }
        var addresses = await _readOnlyaddressRepository.GetUserAddressesAsync(user.Id);
        if (addresses != null && addresses.Any(x => x.AddressLine.Equals(request.AddressLine) || x.Name.Equals(request.Name)))
        {
            throw new ApiException("address_already_exists");
        }
        var defaultAddress = addresses?.FirstOrDefault(x => x.IsDefault);
        if (defaultAddress != null)
        {
            defaultAddress.IsDefault = false;
            _addressRepository.Update(defaultAddress);
        }
        var address = _addressRepository.Add(new Address()
        {
            AddressLine = request.AddressLine,
            Name = request.Name,
            IsDefault = addresses == null || !addresses.Any() || request.IsDefault,
            UserId = user.Id,
        });
        await _addressRepository.SaveChangesAsync();
        return new AddressPayload(address);
    }
}
