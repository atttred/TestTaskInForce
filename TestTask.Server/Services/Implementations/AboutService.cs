using TestTask.Server.Data.Entities;
using TestTask.Server.Data.Repositories.Interfaces;
using TestTask.Server.Services.Interfaces;

namespace TestTask.Server.Services.Implementations;

public class AboutService : IAboutService
{
    private readonly IAboutRepository _aboutRepository;

    public AboutService(IAboutRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    public async Task<AboutContent> GetAboutContentAsync()
    {
        return await _aboutRepository.GetCurrentAsync();
    }

    public async Task UpdateAboutContentAsync(string content, string userId)
    {
        await _aboutRepository.UpdateContentAsync(content, userId);
    }
}
