using BloomAI.API.Data;
using BloomAI.API.Models;
using Microsoft.AspNetCore.Http;

namespace BloomAI.API.Services;

public class AuditService : IAuditService
{
    private readonly BloomAIDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(BloomAIDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(Guid userId, string action, string entityType, Guid entityId, string? oldValues = null, string? newValues = null)
    {
        var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        var auditLog = new AuditLog
        {
            UserId = userId,
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            OldValues = oldValues,
            NewValues = newValues,
            IpAddress = ipAddress,
            CreatedAt = DateTime.UtcNow
        };

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }
}
