using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries.TaskItems;

public class GetTasksQueryHandler
{
    private readonly ITaskRepository _repository;
    
    public GetTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskItem>> Execute(GetTasksQuery query)
    {
        return await _repository.GetAllTasksAsync();
    }
}