using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.CreateTask;
using TaskManagerApi.Application.UseCases.DeleteTask;
using TaskManagerApi.Application.UseCases.UpdateTask;
using TaskManagerApi.Domain.Entities;
using TaskManagerApi.DTOs;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    readonly ITaskRepository _repository;
    readonly UpdateTaskUseCase _updateTaskUseCase;
    readonly CreateTaskUseCase _createTaskUseCase;
    readonly DeleteTaskUseCase _deleteTaskUseCase;

    public TasksController(
        ITaskRepository repository,
        UpdateTaskUseCase updateTaskUseCase,
        CreateTaskUseCase createTaskUseCase,
        DeleteTaskUseCase deleteTaskUseCase)
    {
        _repository = repository;
        _updateTaskUseCase = updateTaskUseCase;
        _createTaskUseCase = createTaskUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
    }

    [HttpGet]
    public async Task<List<TaskItem>> GetTasks()
    {
        return await _repository.GetAllAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var command = new CreateTaskCommand
        {
            Title = dto.Title,
            Description = dto.Description,
        };

        var result = await _createTaskUseCase.Execute(command);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
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
        var command = new DeleteTaskCommand
        {
            Id = id
        };
        
        await _deleteTaskUseCase.Execute(command);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskItem>> Update(Guid id, UpdateTaskDto dto)
    {
        var command = new UpdateTaskCommand
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            IsDone = dto.IsDone
        };

        var result = await _updateTaskUseCase.Execute(command);

        if (result == null)
            return NotFound();

        return NoContent();
    }
}