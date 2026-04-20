using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.UseCases.UpdateTask;

public class UpdateTaskUseCase
{
    readonly ITaskRepository _repository;

    public UpdateTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem?> Execute(UpdateTaskCommand command)
    {
        var task = await _repository.GetByIdAsync(command.Id);

        if (task == null) 
            return null;
        
        task.Title = command.Title;
        task.Description = command.Description;
        task.IsDone = command.IsDone;
        
        await _repository.UpdateAsync(task);
        return task;
    }
}