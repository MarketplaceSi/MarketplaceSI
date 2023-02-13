using Dto.UploadData;

namespace Kernel.Categories.Commands;
public record CategoryUpdateCommand(InputFileData? File, Guid Id, string Name, Guid? ParentId, bool Active) : IRequest<CategoryPayloadBase>;
