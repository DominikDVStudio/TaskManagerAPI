using TaskManagerApi.Application.Exceptions;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries.TaskItems;

public class GetTaskByIdQueryHandler
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem?> Execute(GetTaskByIdQuery query)
    {
        var task = await _repository.GetTaskByIdAsync(query.Id);

       if (task == null)
           throw new KeyNotFoundException($"Task {query.Id} not found");

        if (task.UserId != query.CurrentUserId)
            throw new ForbiddenException("You do not have access to this task");

        return task;
    }
}