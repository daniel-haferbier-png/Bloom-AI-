namespace BloomAI.API.Services;

public interface IAuditService
{
    Task LogAsync(Guid userId, string action, string entityType, Guid entityId, string? oldValues = null, string? newValues = null);
}
