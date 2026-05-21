using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries.TaskItems;

public class GetMyTasksQuery
{
    public int UserId { get; set; }
}