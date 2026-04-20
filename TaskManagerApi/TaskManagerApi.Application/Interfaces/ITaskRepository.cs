using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    
    Task<TaskItem?> GetByIdAsync(Guid id);
    
    Task CreateAsync(TaskItem taskItem);
    
    Task DeleteAsync(TaskItem taskItem);
    
    Task UpdateAsync(TaskItem taskItem);
}