using TestTask.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TestTask.Server.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TestTask.Server.Data.Repositories.Implementations;

public class UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : RepositoryBase<ApplicationUser>(context), IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<IdentityResult> AddAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, "Default");
        return result;
    }

    public async override Task AddAsync(ApplicationUser user)
    {
        await _userManager.CreateAsync(user);
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, string password)
    {
        await _userManager.UpdateAsync(user);
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, password);
        return IdentityResult.Success;
    }

    public async Task<string> DeleteAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded ? "Success" : string.Join(", ", result.Errors.Select(e => e.Description));
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(role);
        return usersInRole.AsEnumerable();
    }
}