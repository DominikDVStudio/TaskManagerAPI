using TaskManagerApi.Application.Auth;
using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.Interfaces;

namespace TaskManagerApi.Application.UseCases.Users.RegisterUser;

public class LoginUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserUseCase(IUserRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResult> Execute(LoginUserCommand command)
    {
        var userEmail = await _repository.GetByEmailAsync(command.Email);

        if (userEmail == null)
            throw new InvalidOperationException("Invalid credentials!");

        bool hashedPassword = _passwordHasher.Verify(command.Password, userEmail.PasswordHash);

        if (!hashedPassword)
            throw new InvalidOperationException("Invalid credentials!");

        var loginResult = new LoginResult
        {
            UserId = userEmail.Id,
            Email = userEmail.Email
        };

        return loginResult;
    }
}