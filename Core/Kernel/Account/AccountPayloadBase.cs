using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Dto.Generic;

namespace MarketplaceSI.Core.Kernel.Account;

public class AccountPayloadBase : Payload
{
    public AccountPayloadBase(User user)
    {
        User = user;
    }
    public AccountPayloadBase(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }

    public User? User { get; set; }
}
