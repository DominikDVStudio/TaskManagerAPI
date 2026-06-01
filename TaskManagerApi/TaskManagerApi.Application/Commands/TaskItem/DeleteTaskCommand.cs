namespace TaskManagerApi.Application.Commands.TaskItem;

public class DeleteTaskCommand
{
    public int Id { get; init; }
    
    public int CurrentUserId { get; init; }
}