using MarketplaceSI.Core.Kernel.Account;

namespace Kernel.Accounts.Commands;

public record AccountLoginCommand(string Email, string Password) : IRequest<AccountTokenPayload>;
