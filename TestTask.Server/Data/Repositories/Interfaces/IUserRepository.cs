using Microsoft.AspNetCore.Identity;
using TestTask.Server.Data.Entities;

namespace TestTask.Server.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByUserNameAsync(string userName);
    Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role);
    Task<IdentityResult> AddAsync(ApplicationUser user, string password);
    Task<IdentityResult> UpdateAsync(ApplicationUser user, string password);
    Task<string> DeleteAsync(ApplicationUser user);
}
