namespace MarketplaceSI.Core.Kernel.Account;
public class AccountVerifyPayload : Payload
{
    public AccountVerifyPayload(bool isConfirm)
    {
        IsConfirm = isConfirm;
    }
    public AccountVerifyPayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }
    public bool IsConfirm { get; set; }
}