using MarketplaceSI.Core.Dto.Errors;

namespace MarketplaceSI.Core.Dto.Generic;

//TODO: Change payload system
public abstract class Payload
{
    protected Payload(IReadOnlyList<Error>? errors = null)
    {
        Errors = errors;
    }

    public IReadOnlyList<Error>? Errors { get; }
}

public class ActionPayload : Payload
{
    public ActionPayload(bool success)
    {
        IsSuccess = success;
    }

    public ActionPayload(IReadOnlyList<Error> errors) : base(errors)
    {
    }

    public bool IsSuccess { get; set; }
}
