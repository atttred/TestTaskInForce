﻿namespace TestTask.Server.Models.Response;

public class AuthResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}
