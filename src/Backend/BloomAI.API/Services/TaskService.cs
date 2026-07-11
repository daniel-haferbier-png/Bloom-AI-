using AutoMapper;
using BloomAI.API.Data;
using BloomAI.API.DTOs;
using BloomAI.API.Models;

namespace BloomAI.API.Services;

public class TaskService : ITaskService
{
    private readonly BloomAIDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;

    public TaskService(BloomAIDbContext context, IMapper mapper, IAuditService auditService)
    {
        _context = context;
        _mapper = mapper;
        _auditService = auditService;
    }

    public async Task<TaskDto> GetTaskByIdAsync(Guid id)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
        return task == null ? throw new InvalidOperationException("Task not found") : _mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request, Guid currentUserId)
    {
        var task = _mapper.Map<TaskItem>(request);
        task.CreatedAt = DateTime.UtcNow;
        task.Status = "Open";
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Create", "Task", task.Id);
        return _mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskRequest request, Guid currentUserId)
    {
        var task = await _context.Tasks.FindAsync(id)
            ?? throw new InvalidOperationException("Task not found");

        if (request.Title != null) task.Title = request.Title;
        if (request.Description != null) task.Description = request.Description;
        if (request.Status != null) task.Status = request.Status;
        if (request.Priority != null) task.Priority = request.Priority;
        if (request.AssignedToId != null) task.AssignedToId = request.AssignedToId;
        if (request.DueDate != null) task.DueDate = request.DueDate;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "Update", "Task", task.Id);
        return _mapper.Map<TaskDto>(task);
    }

    public async Task<TaskDto> UpdateTaskStatusAsync(Guid id, UpdateTaskStatusRequest request, Guid currentUserId)
    {
        var task = await _context.Tasks.FindAsync(id)
            ?? throw new InvalidOperationException("Task not found");

        task.Status = request.Status;
        if (request.Status == "Completed")
        {
            task.CompletedAt = DateTime.UtcNow;
        }
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        await _auditService.LogAsync(currentUserId, "UpdateStatus", "Task", task.Id);
        return _mapper.Map<TaskDto>(task);
    }

    public async Task<List<TaskDto>> GetProjectTasksAsync(Guid projectId)
    {
        var tasks = await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<List<TaskDto>> GetUserTasksAsync(Guid userId)
    {
        var tasks = await _context.Tasks
            .Where(t => t.AssignedToId == userId)
            .OrderByDescending(t => t.DueDate)
            .ToListAsync();
        return _mapper.Map<List<TaskDto>>(tasks);
    }
}
