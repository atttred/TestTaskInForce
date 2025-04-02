using System.ComponentModel.DataAnnotations;

namespace TestTask.Server.Models.Request;

public class CreateShortUrlRequest
{
    [Required]
    [Url]
    public string OriginalUrl { get; set; } = null!;
}
