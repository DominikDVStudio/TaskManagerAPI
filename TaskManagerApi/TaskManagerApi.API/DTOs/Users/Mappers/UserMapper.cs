using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.DTOs.Users.Mappers;

public class UserMapper
{
    public static UserResponseDto ToDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
        };
    }
}