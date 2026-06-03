using Moq;
using TaskManagerApi.Application.Commands.Users;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.Users.RegisterUser;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Tests.UseCases.Users;

public class LoginUserUseCaseTests
{
    [Fact]
    public async Task LoginUserUseCase_ShouldThrowInvalidOperationException_WhenUserDoesNotExist()
    {
        var repositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        repositoryMock
            .Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var useCase =
            new LoginUserUseCase(repositoryMock.Object, passwordHasherMock.Object, jwtTokenGeneratorMock.Object);

        var command = new LoginUserCommand
        {
            Email = "test@wp.pl",
            Password = "password"
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Execute(command));
    }

    [Fact]
    public async Task LoginUserUseCase_ShouldThrowInvalidOperationException_WhenPasswordDoesNotMatch()
    {
        var repositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        repositoryMock
            .Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User
            {
                Email = "test@wp.pl",
                PasswordHash = "testhash"
            });

        var useCase =
            new LoginUserUseCase(repositoryMock.Object, passwordHasherMock.Object, jwtTokenGeneratorMock.Object);

        var command = new LoginUserCommand
        {
            Email = "test@wp.pl",
            Password = "password"
        };

        passwordHasherMock
            .Setup(h => h.Verify(command.Password, It.IsAny<string>()))
            .Returns(false);

        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Execute(command));
    }

    [Fact]
    public async Task LoginUserUseCase_ShouldLoginUser_WhenUserExistsAndPasswordMatches()
    {
        var repositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        repositoryMock
            .Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User
            {
                Id = 1,
                Email = "test@wp.pl",
                PasswordHash = "testhash"
            });

        passwordHasherMock
            .Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        jwtTokenGeneratorMock
            .Setup(g => g.GenerateToken(It.IsAny<User>()))
            .Returns("jwt-token");

        var useCase =
            new LoginUserUseCase(repositoryMock.Object, passwordHasherMock.Object, jwtTokenGeneratorMock.Object);

        var command = new LoginUserCommand
        {
            Email = "test@wp.pl",
            Password = "password"
        };

        var result = await useCase.Execute(command);

        Assert.Equal(1, result.UserId);
        Assert.Equal("test@wp.pl", result.Email);
        Assert.Equal("jwt-token", result.Token);

        repositoryMock.Verify(
            r => r.GetByEmailAsync(It.IsAny<string>()),
            Times.Once);

        passwordHasherMock.Verify(
            h => h.Verify(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);

        jwtTokenGeneratorMock.Verify(
            g => g.GenerateToken(It.IsAny<User>()),
            Times.Once);
    }
}