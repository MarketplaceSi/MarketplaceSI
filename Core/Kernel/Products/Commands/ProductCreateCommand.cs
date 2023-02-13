using Dto.UploadData;
using MarketplaceSI.Core.Dto.Enums;

namespace Kernel.Products.Commands;
public record ProductCreateCommand(List<InputFileData> Files,
    string Title, string Description, double Price, Guid CategoryId, ProductCondition Condition)
    : IRequest<ProductPayloadBase>;