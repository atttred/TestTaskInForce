using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestTask.Server.Models.Request;
using TestTask.Server.Models.Response;
using TestTask.Server.Services.Interfaces;

namespace TestTask.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController : Controller
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAboutContent()
    {
        try
        {
            var content = await _aboutService.GetAboutContentAsync();

            return Ok(new AboutContentResponse
            {
                Content = content.Content,
                LastUpdatedDate = content.LastModified,
                LastUpdatedBy = content.ModifiedById.ToString()
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAboutContent([FromBody] UpdateAboutContentRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            await _aboutService.UpdateAboutContentAsync(request.Content, userId);

            return Ok(new { message = "About content updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
