namespace TaskManagerApi.Application.UseCases.CreateTask;

public class CreateTaskCommand
{
    public string Title { get; set; }
    
    public string Description { get; set; }
}