namespace MarketplaceSI.Core.Dto.Errors;

public record Error(string ErrorMessage, string ErrorCode, string? PropertyName = null);