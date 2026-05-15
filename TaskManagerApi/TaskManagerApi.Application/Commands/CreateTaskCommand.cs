namespace TaskManagerApi.Application.Commands;

public class CreateTaskCommand
{
    public string Title { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
}