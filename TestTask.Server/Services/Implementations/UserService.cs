using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TestTask.Server.Data.Entities;
using TestTask.Server.Data.Repositories.Interfaces;
using TestTask.Server.Services.Interfaces;

namespace TestTask.Server.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
    }

    public async Task<string> RegisterUserAsync(string email, string userName, string password, bool rememberMe)
    {
        var user = new ApplicationUser { UserName = userName, Email = email };
        var result = await _userRepository.AddAsync(user, password);
        if (result.Succeeded)
        {
            await LoginUserAsync(user, password, rememberMe);
            return "Success";
        }
        return string.Join(", ", result.Errors.Select(e => e.Description));
    }

    public async Task<string> LoginUserAsync(string email, string password, bool rememberMe)
    {
        var user = await _userRepository.GetByEmailAsync(email) ?? await _userRepository.GetByUserNameAsync(email);
        if (user == null)
        {
            return "User not found.";
        }
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName!));
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        var result = await LoginUserAsync(user, password, rememberMe);
        if (result.Succeeded)
        {
            return "Success";
        }
        if (result.IsLockedOut)
        {
            return "User is locked out.";
        }
        if (result.IsNotAllowed)
        {
            return "User is not allowed to sign in.";
        }
        if (result.RequiresTwoFactor)
        {
            return "Two-factor authentication is required.";
        }
        return "Invalid email or password.";
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<SignInResult> LoginUserAsync(ApplicationUser user, string password, bool rememberMe)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new InvalidOperationException("UserName cannot be null or empty.");
        }
        return await _signInManager.PasswordSignInAsync(user.UserName, password, rememberMe, lockoutOnFailure: false);
    }

    public async Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (claims == null)
        {
            throw new ArgumentNullException(nameof(claims));
        }

        foreach (var claim in claims)
        {
            var result = await _userManager.AddClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to add claim {claim.Type} to user {user.Email}");
            }
        }
    }
    public async Task RemoveClaimAsync(ApplicationUser user, string claimType)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (string.IsNullOrEmpty(claimType))
        {
            throw new ArgumentNullException(nameof(claimType));
        }

        var existingClaims = await _userManager.GetClaimsAsync(user);

        var claimToRemove = existingClaims.FirstOrDefault(c => c.Type == claimType);
        if (claimToRemove != null)
        {
            var result = await _userManager.RemoveClaimAsync(user, claimToRemove);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to remove claim {claimType} from user {user.Email}");
            }
        }
    }
}
