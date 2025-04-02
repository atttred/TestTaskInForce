namespace TestTask.Server.Models.Response;

public class AboutContentResponse
{
    public string Content { get; set; } = string.Empty;
    public DateTime? LastUpdatedDate { get; set; }
    public string LastUpdatedBy { get; set; } = string.Empty;
}
