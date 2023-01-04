using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Infrastructure.Exceptions;
using MarketplaceSI.Core.Infrastructure.Extensions;
using Microsoft.Extensions.Options;

namespace MarketplaceSI.Web.Api.Graphql.Errors;

public class GraphQLErrorFilter : IErrorFilter
{
    private readonly ExceptionSettings _options;
    private readonly IHttpContextAccessor _context;
    public GraphQLErrorFilter(IOptions<ExceptionSettings> options, IHttpContextAccessor httpContext)
    {
        _options = options.Value;
        _context = httpContext;
    }

    public IError OnError(IError error)
    {
        string? message = string.Empty;
        string? code = string.Empty;
        var cultureKey = _context?.HttpContext?.Request.Headers["Accept-Language"];
        var currentLanguage =
            string.IsNullOrEmpty(cultureKey)
                ? "en"
                : (((string)cultureKey).GetLanguage() ?? "en");
        switch (error.Exception)
        {
            case ValidationException exception:
                ExceptionItem? item = _options?.Items[currentLanguage].Find(i => i.Code == error.Exception.Message);
                message = item?.Message;
                code = item?.Code ?? error.Code;
                break;
            case ApiException exception:
                ExceptionItem? exceptionItem = _options?.Items[currentLanguage].Find(i => i.Code == error.Exception.Message);
                message = exceptionItem?.Message;
                code = exceptionItem?.Code ?? "unhandled_exception";
                break;
            default:
                message = error.Exception?.Message ?? error.Message;
                code = "unhandled_exception";
                break;
        }
        return ErrorBuilder.FromError(error)
                .SetMessage(message ?? error.Message)
                .SetCode(code)
                .Build();
    }
}