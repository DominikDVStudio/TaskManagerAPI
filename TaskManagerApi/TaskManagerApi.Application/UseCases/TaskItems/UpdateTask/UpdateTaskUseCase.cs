using TaskManagerApi.Application.Commands.TaskItem;
using TaskManagerApi.Application.Exceptions;
using TaskManagerApi.Application.Interfaces;

namespace TaskManagerApi.Application.UseCases.TaskItems.UpdateTask;

public class UpdateTaskUseCase
{
    readonly ITaskRepository _repository;

    public UpdateTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(UpdateTaskCommand command)
    {
        var task = await _repository.GetTaskByIdAsync(command.Id);

        if (task == null)
            throw new KeyNotFoundException($"Task {command.Id} not found");

        if (task.UserId != command.CurrentUserId)
            throw new ForbiddenException("You do not have access to this task");
        
        task.Title = command.Title;
        task.Description = command.Description;
        task.IsDone = command.IsDone;
        
        await _repository.UpdateTaskAsync(task);
    }
}