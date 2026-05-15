using TaskManagerApi.Application.Commands;
using TaskManagerApi.Application.Interfaces;

namespace TaskManagerApi.Application.UseCases.DeleteTask;

public class DeleteTaskUseCase
{
    readonly ITaskRepository _repository;

    public DeleteTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(DeleteTaskCommand command)
    {
        var task = await _repository.GetByIdAsync(command.Id);

        if (task == null)
            throw new KeyNotFoundException("Task not found");
        
        await _repository.DeleteAsync(command.Id);
    }
}