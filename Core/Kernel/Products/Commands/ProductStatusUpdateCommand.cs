using MarketplaceSI.Core.Dto.Enums;

namespace Kernel.Products.Commands;
public record ProductStatusUpdateCommand(Guid Id, ProductStatus Status) : IRequest<ProductPayloadBase>;