using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;
using TaskManagerApi.DTOs;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    readonly ITaskRepository _repository;

    public TasksController(ITaskRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<List<TaskItem>> GetTasks()
    {
        return await _repository.GetAllAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            IsDone = false,
        };

        await _repository.CreateAsync(task);

        return Ok(task);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItem>> Get(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskItem>> Update(Guid id, UpdateTaskDto dto)
    {
        var task  = await _repository.GetByIdAsync(id);
        
        if (task == null)
            return NotFound();
        
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.IsDone = dto.IsDone;
        
        await _repository.UpdateAsync(task);
        
        return NoContent();
    }
}