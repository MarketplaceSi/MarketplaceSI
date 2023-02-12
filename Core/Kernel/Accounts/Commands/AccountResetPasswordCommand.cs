namespace Kernel.Accounts.Commandsp;
public record AccountResetPasswordCommand(string Email) : IRequest<ActionPayload>;
