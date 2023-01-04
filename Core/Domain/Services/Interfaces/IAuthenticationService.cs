using System.Security.Principal;

namespace MarketplaceSI.Core.Domain.Services.Interfaces;

public interface IAuthenticationService
{
    Task<IPrincipal> Authenticate(string username, string password);
}
