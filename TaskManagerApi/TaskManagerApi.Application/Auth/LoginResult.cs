namespace TaskManagerApi.Application.Auth;

public class LoginResult
{
    public int UserId { get; set; }

    public string Email { get; set; } = string.Empty;
    
    public string Token { get; set; } = string.Empty;
}