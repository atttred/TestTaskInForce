using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Server.Data.Entities;

public class AboutContent
{
    public Guid Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime LastModified { get; set; }

    public Guid ModifiedById { get; set; }

    [ForeignKey(nameof(ModifiedById))]
    public ApplicationUser ModifiedBy { get; set; } = null!;
}