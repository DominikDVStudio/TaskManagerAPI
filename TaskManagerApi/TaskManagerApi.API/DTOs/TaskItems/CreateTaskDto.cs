namespace TaskManagerApi.DTOs.TaskItems;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}