namespace Kernel.Accounts.Commands;
public record AccountDeleteCommand(string Reason) : IRequest<ActionPayload>;