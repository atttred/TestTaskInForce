﻿namespace TestTask.Server.Models.Request;

public class RegisterRequest
{
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
