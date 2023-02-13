using Microsoft.EntityFrameworkCore;

namespace Kernel.Users.Commands;
public class UserAddressRemoveCommandHandler : IRequestHandler<UserAddressRemoveCommand, ActionPayload>
{
    public readonly IAddressRepository _addressRepository;
    public readonly IUserService _userService;

    public UserAddressRemoveCommandHandler(IUserService userService, IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
        _userService = userService;
    }


    public async Task<ActionPayload> Handle(UserAddressRemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }
        var address = await _addressRepository.GetByIdAsync(request.Id);
        if (address != null)
        {
            await _addressRepository.DeleteAsync(address);
            if (address.IsDefault)
            {
                var adr = await _addressRepository.GetAll().Where(_ => _.UserId == user.Id).FirstOrDefaultAsync();
                if (adr != null)
                {
                    adr.IsDefault = true;
                    _addressRepository.Update(adr);
                    await _addressRepository.SaveChangesAsync();
                }
            }
        }

        return new ActionPayload(true);
    }
}