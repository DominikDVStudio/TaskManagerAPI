using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.UseCases.Users.RegisterUser;

public class RegisterUserUseCase
{
    private readonly IUserRepository _repository;
    
    public RegisterUserUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Execute(RegisterUserCommand command)
    {
        var userEmail = await _repository.GetByEmailAsync(command.Email);
        
        if (userEmail != null)
            throw new InvalidOperationException("User already exists!");
        
        var user = new User
        {
            Username = command.Username,
            Email = command.Email,
            PasswordHash = command.Password,
        };
        
        await _repository.CreateUserAsync(user);

        return user;
    }
}