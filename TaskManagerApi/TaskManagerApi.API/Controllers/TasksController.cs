using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Application.Commands.TaskItem;
using TaskManagerApi.Application.Queries.TaskItems;
using TaskManagerApi.Application.UseCases.TaskItems.CreateTask;
using TaskManagerApi.Application.UseCases.TaskItems.DeleteTask;
using TaskManagerApi.Application.UseCases.TaskItems.UpdateTask;
using TaskManagerApi.Domain.Entities;
using TaskManagerApi.DTOs.TaskItems;
using TaskManagerApi.DTOs.TaskItems.Mappers;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly UpdateTaskUseCase _updateTaskUseCase;
    private readonly CreateTaskUseCase _createTaskUseCase;
    private readonly DeleteTaskUseCase _deleteTaskUseCase;
    private readonly GetTasksQueryHandler _getTasksQueryHandler;
    private readonly GetTaskByIdQueryHandler _getTaskByIdQueryHandler;

    public TasksController(
        UpdateTaskUseCase updateTaskUseCase,
        CreateTaskUseCase createTaskUseCase,
        DeleteTaskUseCase deleteTaskUseCase,
        GetTasksQueryHandler getTasksQueryHandler,
        GetTaskByIdQueryHandler getTaskByIdQueryHandler)
    {
        _updateTaskUseCase = updateTaskUseCase;
        _createTaskUseCase = createTaskUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _getTasksQueryHandler = getTasksQueryHandler;
        _getTaskByIdQueryHandler = getTaskByIdQueryHandler;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskResponseDto>>> GetTasks()
    {
        var query = new GetTasksQuery();

        List<TaskItem> tasks = await _getTasksQueryHandler.Execute(query);

        List<TaskResponseDto> response = tasks
            .Select(TaskMapper.ToDto)
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponseDto>> GetById(int id)
    {
        var query = new GetTaskByIdQuery
        {
            Id = id
        };

        var task = await _getTaskByIdQueryHandler.Execute(query);

        if (task == null)
            return NotFound();

        return Ok(TaskMapper.ToDto(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
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