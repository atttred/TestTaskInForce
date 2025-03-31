using TestTask.Server.Data.Entities;

namespace TestTask.Server.Data.Repositories.Interfaces;

public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
{
    Task<ShortenedUrl> GetByOriginalUrlAsync(string originalUrl);
    Task<ShortenedUrl> GetByShortCodeAsync(string shortCode);
    Task<IEnumerable<ShortenedUrl>> GetByUserIdAsync(Guid userId);
}
