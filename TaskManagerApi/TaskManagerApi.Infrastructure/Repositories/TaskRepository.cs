using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;
using TaskManagerApi.Infrastructure.Data;

namespace TaskManagerApi.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    readonly AppDbContext _dbContext;

    public TaskRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateTaskAsync(TaskItem taskItem)
    {
        await _dbContext.Tasks.AddAsync(taskItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _dbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);
       
       if (task == null)
           throw new Exception($"Task with id: {id} not found");
       
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskItem updatedTask)
    {
        _dbContext.Tasks.Update(updatedTask);
        await _dbContext.SaveChangesAsync();
    }
}