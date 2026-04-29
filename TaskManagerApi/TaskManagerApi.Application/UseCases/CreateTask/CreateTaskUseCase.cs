using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.UseCases.CreateTask;

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
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
        };
        
       if (string.IsNullOrWhiteSpace(command.Title))
           throw new ArgumentException("Title cannot be empty!");

        await _repository.CreateAsync(task);

        return task;
    }
}