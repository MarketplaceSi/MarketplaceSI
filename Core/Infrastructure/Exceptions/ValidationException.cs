using System.Net;

namespace MarketplaceSI.Core.Infrastructure.Exceptions;

public class ValidationException : ApiException
{
    public string? ErrorCode { get; set; }
    public string? PropertyName { get; set; }
    public ValidationException(List<string> errors = default!)
        : base("Validation Failures Occured.", errors, HttpStatusCode.BadRequest)
    {
    }

    public ValidationException()
    {
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string message, string errorCode, string propertyName) : base(message)
    {
        ErrorCode = errorCode;
        PropertyName = propertyName;
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public ValidationException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, statusCode)
    {
    }

    public ValidationException(string message, List<string> errors = default!, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, errors, statusCode)
    {
    }
}
