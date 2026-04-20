namespace TaskManagerApi.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;   
    
    public string Description { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;
}