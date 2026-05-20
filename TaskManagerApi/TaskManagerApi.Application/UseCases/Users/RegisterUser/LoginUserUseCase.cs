using TaskManagerApi.Application.Auth;
using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.Interfaces;

namespace TaskManagerApi.Application.UseCases.Users.RegisterUser;

public class LoginUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserUseCase(IUserRepository repository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResult> Execute(LoginUserCommand command)
    {
        var user = await _repository.GetByEmailAsync(command.Email);

        if (user == null)
            throw new InvalidOperationException("Invalid credentials!");

        bool hashedPassword = _passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!hashedPassword)
            throw new InvalidOperationException("Invalid credentials!");
        
        string token = _jwtTokenGenerator.GenerateToken(user);

        var loginResult = new LoginResult
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token
        };

        return loginResult;
    }
}