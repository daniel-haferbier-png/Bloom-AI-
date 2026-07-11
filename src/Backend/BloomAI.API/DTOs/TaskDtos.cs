namespace BloomAI.API.DTOs;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public Guid? AssignedToId { get; set; }
    public Guid? LocationId { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
    public string Priority { get; set; } = "Medium";
    public Guid? AssignedToId { get; set; }
    public Guid? LocationId { get; set; }
    public DateTime? DueDate { get; set; }
}

public class UpdateTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public Guid? AssignedToId { get; set; }
    public DateTime? DueDate { get; set; }
}

public class UpdateTaskStatusRequest
{
    public string Status { get; set; } = string.Empty;
}
