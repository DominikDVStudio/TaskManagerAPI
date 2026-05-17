using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    
    Task<User?> GetUserByIdAsync(int id);   
    
    Task<User?> GetByEmailAsync(string email);
    
    Task CreateUserAsync(User user);
    
    Task UpdateUserAsync(User user);
    
    Task DeleteUserAsync(int id);
}