using MarketplaceSI.Core.Domain.Constants;

namespace MarketplaceSI.Core.Infrastructure.Extensions;

public static class LanguageExtensions
{
    public static string? GetLanguage(this string langInfo) =>
        Constants.SupportLanguages.Find(l => langInfo.Contains(l));
}