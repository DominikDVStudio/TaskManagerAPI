using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    
    Task<TaskItem?> GetByIdAsync(int id);
    
    Task CreateAsync(TaskItem taskItem);
    
    Task DeleteAsync(int id);
    
    Task UpdateAsync(TaskItem taskItem);
}