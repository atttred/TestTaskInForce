using System.ComponentModel.DataAnnotations;

namespace TestTask.Server.Models.Request;

public class UpdateAboutContentRequest
{
    [Required]
    public string Content { get; set; } = null!;
}
