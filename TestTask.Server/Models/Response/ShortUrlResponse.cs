namespace TestTask.Server.Models.Response;

public class ShortUrlResponse
{
    public Guid Id { get; set; }
    public string OriginalUrl { get; set; } = null!;
    public string ShortCode { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; } = null!;
}
