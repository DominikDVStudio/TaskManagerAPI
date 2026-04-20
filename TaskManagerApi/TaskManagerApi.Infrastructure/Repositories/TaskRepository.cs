using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    readonly List<TaskItem> _tasks = [];

    public Task<List<TaskItem>> GetAllAsync()
    {
        return Task.FromResult(_tasks);
    }

    public Task<TaskItem?> GetByIdAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task CreateAsync(TaskItem taskItem)
    {
        _tasks.Add(taskItem);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
            throw new Exception($"Task with id: {id} not found");

        _tasks.Remove(task);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TaskItem updatedTask)
    {
        return Task.CompletedTask;
    }
}