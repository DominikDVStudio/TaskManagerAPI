using TaskManagerApi.Application.Commands.TaskItem;
using TaskManagerApi.Application.Exceptions;
using TaskManagerApi.Application.Interfaces;

namespace TaskManagerApi.Application.UseCases.TaskItems.DeleteTask;

public class DeleteTaskUseCase
{
    readonly ITaskRepository _repository;

    public DeleteTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(DeleteTaskCommand command)
    {
        var task = await _repository.GetTaskByIdAsync(command.Id);

        if (task == null)
            throw new KeyNotFoundException($"Task {command.Id} not found");

        if (task.UserId != command.CurrentUserId)
            throw new ForbiddenException("You do not have access to this task");
        
        await _repository.DeleteTaskAsync(command.Id);
    }
}