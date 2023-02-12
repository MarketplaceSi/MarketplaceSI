using MarketplaceSI.Core.Kernel.Account;

namespace Kernel.Accounts.Commands;

public record AccountRenewTokenCommand(string AccessToken, string RefreshToken) : IRequest<AccountTokenPayload>;
