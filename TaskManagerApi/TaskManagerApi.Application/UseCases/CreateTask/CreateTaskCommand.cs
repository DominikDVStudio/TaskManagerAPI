namespace TaskManagerApi.Application.UseCases.CreateTask;

public class CreateTaskCommand
{
    public string Title { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
}