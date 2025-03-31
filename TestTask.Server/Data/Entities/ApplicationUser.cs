using Microsoft.AspNetCore.Identity;

namespace TestTask.Server.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<ShortenedUrl> ShortenedUrls { get; set; } = new List<ShortenedUrl>();
    public ICollection<AboutContent> ModifiedAboutContents { get; set; } = new List<AboutContent>();
}
