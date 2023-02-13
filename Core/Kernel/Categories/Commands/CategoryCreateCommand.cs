using Dto.UploadData;

namespace Kernel.Categories.Commands;
public record CategoryCreateCommand(InputFileData? File, string Name, Guid? ParentId) : IRequest<CategoryPayloadBase>;
