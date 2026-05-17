using TaskManagerApi.Application.Commands;
using TaskManagerApi.Application.Commands.TaskItem;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.UseCases.TaskItems.CreateTask;

public class CreateTaskUseCase
{
    readonly ITaskRepository _repository;

    public CreateTaskUseCase(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem> Execute(CreateTaskCommand command)
    {
        var task = new TaskItem
        {
            Title = command.Title,
            Description = command.Description,
        };

        await _repository.CreateTaskAsync(task);

        return task;
    }
}