using TestTask.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TestTask.Server.Data.Repositories.Interfaces;

namespace TestTask.Server.Data.Repositories.Implementations;

public class AboutRepository(ApplicationDbContext context) : RepositoryBase<AboutContent>(context), IAboutRepository
{
    public async Task<AboutContent> GetCurrentAsync()
    {
        return await _context.AboutContent.OrderByDescending(a => a.LastModified).FirstOrDefaultAsync() ??
                          new AboutContent { Id = Guid.NewGuid(), Content = "Default description", LastModified = DateTime.UtcNow };
    }

    public async Task UpdateContentAsync(string content, string userId)
    {
        var about = await GetCurrentAsync();
        about.Content = content;
        about.LastModified = DateTime.UtcNow;
        about.ModifiedById = Guid.Parse(userId);
        _context.AboutContent.Update(about);
        await _context.SaveChangesAsync();
    }
}