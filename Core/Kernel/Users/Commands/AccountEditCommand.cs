using Dto.UploadData;

namespace Kernel.Users.Commands;
public record AccountEditCommand(InputFileData? File, string? FirstName, string? LastName, string? Description, DateTime? DateOfBirth, string? PhoneNumber) : IRequest<User>;
