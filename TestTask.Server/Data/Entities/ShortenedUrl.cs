using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Server.Data.Entities;

public class ShortenedUrl
{
    public Guid Id { get; set; }

    [Required]
    public string OriginalUrl { get; set; } = string.Empty;

    [Required]
    public string ShortCode { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Guid CreatedById { get; set; }

    [ForeignKey(nameof(CreatedById))]
    public ApplicationUser CreatedBy { get; set; } = null!;
}