namespace Kernel.Accounts.Commands;
public record AccountSetPasswordCommand(string Key, string Password) : IRequest<ActionPayload>;
