using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}