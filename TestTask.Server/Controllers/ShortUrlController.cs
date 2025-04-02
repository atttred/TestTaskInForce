using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestTask.Server.Models.Request;
using TestTask.Server.Models.Response;
using TestTask.Server.Services.Interfaces;

namespace TestTask.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ShortUrlController : Controller
{
    private readonly IShortUrlService _shortUrlService;

    public ShortUrlController(IShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        try
        {
            string? userId = User.Identity!.IsAuthenticated ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value : null;
            var urls = await _shortUrlService.GetUserUrlsAsync(userId!);

            var response = urls.Select(url => new ShortUrlResponse
            {
                Id = url.Id,
                OriginalUrl = url.OriginalUrl,
                ShortCode = url.ShortCode,
                CreatedDate = url.CreatedDate,
                CreatedById = url.CreatedById.ToString()
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{shortCode}")]
    [Authorize]
    public async Task<IActionResult> GetUrlDetails(string shortCode)
    {
        try
        {
            var url = await _shortUrlService.GetByShortCodeAsync(shortCode);

            if (url == null)
            {
                return NotFound(new { message = "URL not found" });
            }

            return Ok(new ShortUrlResponse
            {
                Id = url.Id,
                OriginalUrl = url.OriginalUrl,
                ShortCode = url.ShortCode,
                CreatedDate = url.CreatedDate,
                CreatedById = url.CreatedById.ToString()
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateShortUrl([FromBody] CreateShortUrlRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var url = await _shortUrlService.CreateShortUrlAsync(request.OriginalUrl, userId);

            return Ok(new ShortUrlResponse
            {
                Id = url.Id,
                OriginalUrl = url.OriginalUrl,
                ShortCode = url.ShortCode,
                CreatedDate = url.CreatedDate,
                CreatedById = url.CreatedById.ToString()
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{shortCode}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl(string shortCode)
    {
        try
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            bool isAdmin = User.IsInRole("Admin");

            await _shortUrlService.DeleteUrlAsync(shortCode, userId, isAdmin);

            return Ok(new { message = "URL deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("redirect/{shortCode}")]
    public async Task<IActionResult> RedirectToOriginal(string shortCode)
    {
        try
        {
            var url = await _shortUrlService.GetByShortCodeAsync(shortCode);

            if (url == null)
            {
                return NotFound(new { message = "URL not found" });
            }

            return Redirect(url.OriginalUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
