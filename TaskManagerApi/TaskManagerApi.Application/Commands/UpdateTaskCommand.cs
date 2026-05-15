namespace TaskManagerApi.Application.Commands;

public class UpdateTaskCommand
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsDone { get; init; }
}