using System.Net;
using System.Text.Json;

namespace BloomAI.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode = HttpStatusCode.InternalServerError,
            message = exception.Message,
            error = exception.GetType().Name
        };

        if (exception is InvalidOperationException)
        {
            response = new
            {
                statusCode = HttpStatusCode.BadRequest,
                message = exception.Message,
                error = "InvalidOperationException"
            };
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
