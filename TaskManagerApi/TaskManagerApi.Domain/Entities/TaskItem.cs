namespace TaskManagerApi.Domain.Entities;

public class TaskItem
{
    public int Id { get; init; }
    
    public string Title { get; set; } = string.Empty;   
    
    public string Description { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;

    public int UserId { get; init; }
    public User? User { get; init; }
}