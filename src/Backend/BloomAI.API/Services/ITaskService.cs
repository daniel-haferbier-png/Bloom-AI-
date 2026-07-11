using BloomAI.API.DTOs;

namespace BloomAI.API.Services;

public interface ITaskService
{
    Task<TaskDto> GetTaskByIdAsync(Guid id);
    Task<TaskDto> CreateTaskAsync(CreateTaskRequest request, Guid currentUserId);
    Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskRequest request, Guid currentUserId);
    Task<TaskDto> UpdateTaskStatusAsync(Guid id, UpdateTaskStatusRequest request, Guid currentUserId);
    Task<List<TaskDto>> GetProjectTasksAsync(Guid projectId);
    Task<List<TaskDto>> GetUserTasksAsync(Guid userId);
}
