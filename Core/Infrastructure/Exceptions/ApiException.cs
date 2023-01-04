using System.Net;

namespace MarketplaceSI.Core.Infrastructure.Exceptions;

public class ApiException : Exception
{
    public List<string> ErrorMessages { get; } = new();
    public HttpStatusCode StatusCode { get; }
    
    public ApiException() : base()
    {
    }

    public ApiException(string? message) : base(message)
    {
    }

    public ApiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : base(message)
    {
        StatusCode = statusCode;
    }

    public ApiException(string message, List<string> errors = default!, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

}