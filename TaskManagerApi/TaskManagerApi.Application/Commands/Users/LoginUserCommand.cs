namespace TaskManagerApi.Application.Commands.Users;

public class LoginUserCommand
{
    public required string Email { get; init; }
    
    public required string Password { get; init; }
}