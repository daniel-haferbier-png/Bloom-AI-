using System.Security.Claims;

namespace BloomAI.API.Middleware;

public class AuditLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditLoggingMiddleware> _logger;

    public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
        var method = context.Request.Method;
        var path = context.Request.Path;
        var timestamp = DateTime.UtcNow;

        _logger.LogInformation(
            "Request: UserId={UserId}, Method={Method}, Path={Path}, Timestamp={Timestamp}",
            userId, method, path, timestamp
        );

        await _next(context);

        _logger.LogInformation(
            "Response: UserId={UserId}, StatusCode={StatusCode}, Path={Path}",
            userId, context.Response.StatusCode, path
        );
    }
}
