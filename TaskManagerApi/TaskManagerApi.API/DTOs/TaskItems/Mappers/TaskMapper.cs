using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.DTOs.TaskItems.Mappers;

public static class TaskMapper
{
    public static TaskResponseDto ToDto(TaskItem task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsDone = task.IsDone
        };
    }
}