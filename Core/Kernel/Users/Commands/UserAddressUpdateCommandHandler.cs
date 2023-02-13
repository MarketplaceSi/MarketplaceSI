using Microsoft.EntityFrameworkCore;

namespace Kernel.Users.Commands;
public class UserAddressUpdateCommandHandler : IRequestHandler<UserAddressUpdateCommand, AddressPayload>
{
    private readonly IUserService _userService;
    private readonly IAddressRepository _addressRepository;

    public UserAddressUpdateCommandHandler(IUserService userService, IAddressRepository addressRepository)
    {
        _userService = userService;
        _addressRepository = addressRepository;
    }


    public async Task<AddressPayload> Handle(UserAddressUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();
        if (user == null)
        {
            throw new ApiException();
        }

        var addresses = await _addressRepository.GetAll().Where(x => x.UserId == user.Id).ToListAsync();

        if (addresses.Any(_ => (_.Name.Equals(request.Name) || _.AddressLine.Equals(request.AddressLine)) && _.Id != request.Id))
        {
            throw new ApiException("address_alredy_exists");
        }

        var addres = new Address();
        if (request.Id == null)
        {
            addres = _addressRepository.Add(new Address()
            {
                AddressLine = request.AddressLine,
                Name = request.Name,
                UserId = user.Id,
            });
        }
        else
        {
            addres = addresses.FirstOrDefault(x => x.Id == request.Id);
            if (addres == null)
            {
                throw new ApiException("access_forbidden");
            }

            addres.Name = request.Name;
            addres.AddressLine = request.AddressLine;
        }


        if (addres.IsDefault != request.IsDefault && addresses.Count > 1)
        {
            if (addres.IsDefault)
            {
                addresses.First(x => x.Id != request.Id).IsDefault = true;
            }
            else
            {
                var def = addresses.FirstOrDefault(x => x.IsDefault && x.Id != request.Id);
                if (def != null)
                {
                    def.IsDefault = false;
                }
            }
            addres.IsDefault = request.IsDefault;
        }


        _addressRepository.UpdateRange(addresses.ToArray());
        await _addressRepository.SaveChangesAsync();

        return new AddressPayload(addres);
    }
}
