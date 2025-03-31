using TestTask.Server.Data.Entities;

namespace TestTask.Server.Data.Repositories.Interfaces;

public interface IAboutRepository : IRepository<AboutContent>
{
    Task<AboutContent> GetCurrentAsync();
    Task UpdateContentAsync(string content, string userId);
    Task<AboutContent> GetByIdAsync(Guid id);
}
