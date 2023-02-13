using Dto.UploadData;
using MarketplaceSI.Core.Dto.Enums;

namespace Kernel.Products.Commands;
public record ProductUpdateCommand(List<InputFileData>? FilesToUpload, List<string>? FilesToDelete, Guid Id, string Title, string Description, double Price, Guid CategoryId, ProductCondition Condition)
    : IRequest<ProductPayloadBase>;
