namespace MarketplaceSI.Core.Kernel.Account.Commands;

public record AccountCreateCommand(string Email, string FirstName, string LastName, DateTime? DateOfBirth) : IRequest<AccountPayloadBase>;
