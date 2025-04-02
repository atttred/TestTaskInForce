namespace TestTask.Server.Models.Response;

public class CurrentUserResponse
{
    public bool IsAuthenticated { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}
