using System.Security.Claims;
using TestTask.Server.Data.Entities;

namespace TestTask.Server.Services.Interfaces;

public interface IUserService
{
    Task<string> RegisterUserAsync(string email, string userName, string password, bool rememberMe);
    Task<string> LoginUserAsync(string email, string password, bool rememberMe);
    Task LogoutUserAsync();
    Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims);
    Task RemoveClaimAsync(ApplicationUser user, string claimType);
}
