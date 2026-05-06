using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries;

public class GetTaskByIdQueryHandler
{
    private readonly ITaskRepository _repository;
    
    public GetTaskByIdQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem?> Execute(GetTaskByIdQuery query)
    {
        return await _repository.GetByIdAsync(query.Id);
    }
}