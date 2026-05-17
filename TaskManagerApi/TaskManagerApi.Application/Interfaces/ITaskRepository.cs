using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllTasksAsync();
    
    Task<TaskItem?> GetTaskByIdAsync(int id);
    
    Task CreateTaskAsync(TaskItem taskItem);
    
    Task DeleteTaskAsync(int id);
    
    Task UpdateTaskAsync(TaskItem taskItem);
}