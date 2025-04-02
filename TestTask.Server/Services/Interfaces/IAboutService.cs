using TestTask.Server.Data.Entities;

namespace TestTask.Server.Services.Interfaces;
public interface IAboutService
{
    Task<AboutContent> GetAboutContentAsync();
    Task UpdateAboutContentAsync(string content, string userId);
}