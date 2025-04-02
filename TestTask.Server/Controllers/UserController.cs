using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestTask.Server.Models.Request;
using TestTask.Server.Models.Response;
using TestTask.Server.Services.Interfaces;
namespace TestTask.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUserAsync(model.Email, model.UserName, model.Password, model.RememberMe);

        if (result == "Success")
        {
            return Ok(new AuthResponse { IsSuccess = true, Message = "User registered successfully." });
        }

        return BadRequest(new AuthResponse { IsSuccess = false, Message = result });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.LoginUserAsync(model.Email, model.Password, model.RememberMe);

        if (result == "Success")
        {
            return Ok(new AuthResponse { IsSuccess = true, Message = "User logged in successfully." });
        }

        return BadRequest(new AuthResponse { IsSuccess = false, Message = result });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutUserAsync();
        return Ok(new AuthResponse { IsSuccess = true, Message = "User logged out successfully." });
    }

    [HttpGet("current")]
    public IActionResult GetCurrentUser()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return Ok(new CurrentUserResponse { IsAuthenticated = false });
        }

        return Ok(new CurrentUserResponse
        {
            IsAuthenticated = true,
            UserName = User.FindFirstValue(ClaimTypes.Name)!,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            IsAdmin = User.IsInRole("Admin")
        });
    }
}
