using Microsoft.AspNetCore.Authorization;
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
public class TasksController : BaseController
{
    private readonly UpdateTaskUseCase _updateTaskUseCase;
    private readonly CreateTaskUseCase _createTaskUseCase;
    private readonly DeleteTaskUseCase _deleteTaskUseCase;

    private readonly GetTasksQueryHandler _getTasksQueryHandler;
    private readonly GetTaskByIdQueryHandler _getTaskByIdQueryHandler;
    private readonly GetMyTasksQueryHandler _getMyTasksQueryHandler;

    public TasksController(
        UpdateTaskUseCase updateTaskUseCase,
        CreateTaskUseCase createTaskUseCase,
        DeleteTaskUseCase deleteTaskUseCase,
        GetTasksQueryHandler getTasksQueryHandler,
        GetTaskByIdQueryHandler getTaskByIdQueryHandler,
        GetMyTasksQueryHandler getMyTasksQueryHandler)
    {
        _updateTaskUseCase = updateTaskUseCase;
        _createTaskUseCase = createTaskUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _getTasksQueryHandler = getTasksQueryHandler;
        _getTaskByIdQueryHandler = getTaskByIdQueryHandler;
        _getMyTasksQueryHandler = getMyTasksQueryHandler;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<TaskResponseDto>>> GetUserTasks()
    {
        var query = new GetMyTasksQuery
        {
            UserId = CurrentLoggedUserId
        };

        List<TaskItem> tasks = await _getMyTasksQueryHandler.Execute(query);

        List<TaskResponseDto> response = tasks
            .Select(TaskMapper.ToDto)
            .ToList();

        return Ok(response);
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<ActionResult<List<TaskResponseDto>>> GetAllTasks()
    {
        var query = new GetTasksQuery();

        List<TaskItem> tasks = await _getTasksQueryHandler.Execute(query);

        List<TaskResponseDto> response = tasks
            .Select(TaskMapper.ToDto)
            .ToList();

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponseDto>> GetUserTaskById(int id)
    {
        var query = new GetTaskByIdQuery
        {
            Id = id,
            CurrentUserId = CurrentLoggedUserId
        };

        var task = await _getTaskByIdQueryHandler.Execute(query);

        return Ok(TaskMapper.ToDto(task!));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        var command = new CreateTaskCommand
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = CurrentLoggedUserId,
        };

        var result = await _createTaskUseCase.Execute(command);

        return CreatedAtAction(
            nameof(GetUserTaskById),
            new { id = result.Id },
            TaskMapper.ToDto(result));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteTaskCommand
        {
            Id = id,
            CurrentUserId = CurrentLoggedUserId
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
            IsDone = dto.IsDone,
            CurrentUserId = CurrentLoggedUserId,
        };

        await _updateTaskUseCase.Execute(command);

        return NoContent();
    }
}