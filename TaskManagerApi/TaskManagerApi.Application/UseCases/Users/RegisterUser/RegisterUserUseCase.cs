using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.UseCases.Users.RegisterUser;

public class RegisterUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserUseCase(IUserRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> Execute(RegisterUserCommand command)
    {
        var userEmail = await _repository.GetByEmailAsync(command.Email);

        if (userEmail != null)
            throw new InvalidOperationException("User already exists!");

        var user = new User
        {
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
        };

        await _repository.CreateUserAsync(user);

        return user;
    }
}