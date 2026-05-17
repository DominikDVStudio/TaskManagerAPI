using TaskManagerApi.Application.Commands;
using TaskManagerApi.Application.Commands.TaskItem;
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
            throw new KeyNotFoundException("Task not found");
        
        await _repository.DeleteTaskAsync(command.Id);
    }
}