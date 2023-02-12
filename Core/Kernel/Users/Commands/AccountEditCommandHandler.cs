namespace Kernel.Users.Commands;
public class AccountEditCommandHandler : IRequestHandler<AccountEditCommand, User>
{
    private readonly IUserService _userService;
    private readonly IStorageService _storageService;
    public AccountEditCommandHandler(IUserService userService, IStorageService storageService)
    {
        _userService = userService;
        _storageService = storageService;
    }


    public async Task<User> Handle(AccountEditCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserWithUserRoles();

        if (user == null)
        {
            throw new ApiException("access_forbidden");
        }
        if (request.File?.FileStream != null)
        {
            await _storageService.InvalidateFileLink("avatar", user.GetPath());
            user.Avatar = await _storageService.UploadImageAsync(request.File.FileStream, "avatar", user.GetPath());
        }
        user.FirstName = !string.IsNullOrEmpty(request.FirstName) ? request.FirstName : user.FirstName;
        user.LastName = !string.IsNullOrEmpty(request.LastName) ? request.LastName : user.LastName;
        user.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : user.Description;
        user.DateOfBirth = request.DateOfBirth != null ? request.DateOfBirth : user.DateOfBirth;
        user.PhoneNumber = !string.IsNullOrEmpty(request.PhoneNumber) ? request.PhoneNumber : user.PhoneNumber;
        user = await _userService.UpdateUser(user);

        return user;
    }
}