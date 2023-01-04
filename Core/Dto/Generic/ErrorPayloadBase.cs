using MarketplaceSI.Core.Dto.Errors;

namespace MarketplaceSI.Core.Dto.Generic;

public class ErrorPayloadBase : Payload
{
    public ErrorPayloadBase(string message, string code, string? propertyName = null) :
            base(new List<Error>() { new Error(message, code, propertyName) })
    {
    }
}
