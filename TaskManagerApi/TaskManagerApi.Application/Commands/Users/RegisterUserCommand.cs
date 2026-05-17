namespace TaskManagerApi.Application.Commands.Users;

public class RegisterUserCommand
{
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public required string Password { get; set; }
}