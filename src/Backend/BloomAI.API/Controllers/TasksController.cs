using BloomAI.API.DTOs;
using BloomAI.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloomAI.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    private Guid GetCurrentUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
    }

    /// <summary>
    /// Get task by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetTask(Guid id)
    {
        try
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return Ok(task);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskRequest request)
    {
        var userId = GetCurrentUserId();
        var task = await _taskService.CreateTaskAsync(request, userId);
        _logger.LogInformation("Task created: {TaskId}", task.Id);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    /// <summary>
    /// Update task
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTask(Guid id, [FromBody] UpdateTaskRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            var task = await _taskService.UpdateTaskAsync(id, request, userId);
            return Ok(task);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update task status
    /// </summary>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTaskStatus(Guid id, [FromBody] UpdateTaskStatusRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            var task = await _taskService.UpdateTaskStatusAsync(id, request, userId);
            return Ok(task);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Get user's tasks
    /// </summary>
    [HttpGet("user/my-tasks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TaskDto>>> GetUserTasks()
    {
        var userId = GetCurrentUserId();
        var tasks = await _taskService.GetUserTasksAsync(userId);
        return Ok(tasks);
    }
}
