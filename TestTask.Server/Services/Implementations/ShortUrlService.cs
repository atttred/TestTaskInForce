using TestTask.Server.Data.Entities;
using TestTask.Server.Data.Repositories.Interfaces;
using TestTask.Server.Services.Interfaces;

namespace TestTask.Server.Services.Implementations;

public class ShortUrlService : IShortUrlService
{
    private readonly IShortenedUrlRepository _shortenedUrlRepository;

    public ShortUrlService(IShortenedUrlRepository shortenedUrlRepository)
    {
        _shortenedUrlRepository = shortenedUrlRepository;
    }

    public async Task<ShortenedUrl> CreateShortUrlAsync(string originalUrl, string userId)
    {
        var existing = await _shortenedUrlRepository.GetByOriginalUrlAsync(originalUrl);
        if (existing != null)
        {
            return existing;
        }

        var shortCode = GenerateShortCode();
        var url = new ShortenedUrl
        {
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreatedById = Guid.Parse(userId),
            CreatedDate = DateTime.UtcNow,
        };

        await _shortenedUrlRepository.AddAsync(url);
        return url;
    }

    public async Task<ShortenedUrl> GetByShortCodeAsync(string shortCode)
    {
        return await _shortenedUrlRepository.GetByShortCodeAsync(shortCode);
    }

    public async Task<IEnumerable<ShortenedUrl>> GetUserUrlsAsync(string userId)
    {
        return await _shortenedUrlRepository.GetByUserIdAsync(userId);
    }

    public async Task DeleteUrlAsync(string shortCode, string userId, bool isAdmin)
    {
        var url = await _shortenedUrlRepository.GetByShortCodeAsync(shortCode) ?? throw new Exception("URL not found");
        if (isAdmin || url.CreatedById == Guid.Parse(userId))
        {
            await _shortenedUrlRepository.DeleteAsync(url.Id);
            return;
        }

        throw new Exception("You are not allowed to delete this URL");
    }

    private static string GenerateShortCode()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())[..6];
    }
}
