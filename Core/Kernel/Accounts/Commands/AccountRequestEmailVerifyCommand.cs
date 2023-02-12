namespace Kernel.Accounts.Commands;

public record AccountRequestEmailVerifyCommand(string Key) : IRequest<ActionPayload>;
