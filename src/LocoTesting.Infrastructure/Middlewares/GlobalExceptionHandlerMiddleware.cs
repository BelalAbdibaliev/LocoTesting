using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, 
        ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            _logger.LogError(ex, "Произошла ошибка: {Message}", ex.Message);
            await HandleErrorAsync(context, ex);
        }
    }

    private static async Task HandleErrorAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

       context.Response.StatusCode = ex switch
        {
            ArgumentNullException or ArgumentException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException or FileNotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };
       
        var response = new
        {
            Status = context.Response.StatusCode,
            Message = ex.Message,
            TraceId = context.TraceIdentifier
        };

        var result = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }
}