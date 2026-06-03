using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries.TaskItems;

public class GetMyTasksQueryHandler
{
    private readonly ITaskRepository _repository;

    public GetMyTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskItem>> Execute(GetMyTasksQuery query)
    {
        return await _repository.GetTasksByUserIdAsync(query.UserId);
    }
}