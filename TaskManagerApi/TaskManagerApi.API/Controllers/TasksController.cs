using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.CreateTask;
using TaskManagerApi.Application.UseCases.DeleteTask;
using TaskManagerApi.Application.UseCases.UpdateTask;
using TaskManagerApi.Domain.Entities;
using TaskManagerApi.DTOs;
using TaskManagerApi.DTOs.Mappers;

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return NotFound();

        return Ok(TaskMapper.ToDto(task));
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

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id }, 
            TaskMapper.ToDto(result));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteTaskCommand
        {
            Id = id
        };
        
        await _deleteTaskUseCase.Execute(command);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
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