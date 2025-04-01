using TestTask.Server.Data.Entities;

namespace TestTask.Server.Services.Interfaces;

public interface IShortUrlService
{
    Task<ShortenedUrl> CreateShortUrlAsync(string originalUrl, string userId);
    Task<ShortenedUrl> GetByShortCodeAsync(string shortCode);
    Task<IEnumerable<ShortenedUrl>> GetUserUrlsAsync(string userId);
    Task DeleteUrlAsync(string shortCode, string userId, bool isAdmin);
}
