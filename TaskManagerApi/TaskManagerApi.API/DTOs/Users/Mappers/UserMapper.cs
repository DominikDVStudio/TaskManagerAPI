using TaskManagerApi.Application.Auth;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.DTOs.Users.Mappers;

public class UserMapper
{
    public static UserResponseDto UserToDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
        };
    }

    public static LoginResponseDto LoginResultToDto(LoginResult loginResult)
    {
        return new LoginResponseDto
        {
            UserId = loginResult.UserId,
            Email = loginResult.Email
        };
    }
}