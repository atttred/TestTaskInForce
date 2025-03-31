using TestTask.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TestTask.Server.Data.Repositories.Interfaces;

namespace TestTask.Server.Data.Repositories.Implementations;

public class ShortenedUrlRepository(ApplicationDbContext context) : RepositoryBase<ShortenedUrl>(context), IShortenedUrlRepository
{
    public async Task<ShortenedUrl> GetByOriginalUrlAsync(string originalUrl)
    {
        var result =  await _context.ShortenedUrls.FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl);
        return result!;
    }

    public async Task<ShortenedUrl> GetByShortCodeAsync(string shortCode)
    {
        var result = await _context.ShortenedUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
        return result!;
    }

    public async Task<IEnumerable<ShortenedUrl>> GetByUserIdAsync(string userId)
    {
        return await _context.ShortenedUrls.Where(x => x.CreatedById == Guid.Parse(userId)).ToListAsync();
    }
}
