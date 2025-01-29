using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LocoTesting.Infrastructure.Services;

/// <summary>
/// Глобальный обработчик исключений.
/// </summary>
public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    private static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodes = new()
    {
        { typeof(ValidationException), HttpStatusCode.BadRequest },
        { typeof(ArgumentException), HttpStatusCode.BadRequest },
        { typeof(ArgumentNullException), HttpStatusCode.BadRequest },
        { typeof(ArgumentOutOfRangeException), HttpStatusCode.BadRequest },
        { typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized },
        { typeof(KeyNotFoundException), HttpStatusCode.NotFound },
        { typeof(NotImplementedException), HttpStatusCode.NotImplemented },
        { typeof(TimeoutException), HttpStatusCode.GatewayTimeout }
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred.");

        var problemDetails = CreateProblemDetails(context, exception);
        var json = ToJson(problemDetails);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        
        await context.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        // Получаем HTTP-статус по типу исключения
        var statusCode = GetStatusCodeForException(exception);

        var reasonPhrase = ReasonPhrases.GetReasonPhrase((int)statusCode) ?? UnhandledExceptionMsg;

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = reasonPhrase
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = new Dictionary<string, string[]>
            {
                ["ValidationErrors"] = new[] { validationException.Message }
            };
        }
        else if (exception is ArgumentException argumentException)
        {
            problemDetails.Detail = argumentException.Message;
        }
        else if (exception is KeyNotFoundException)
        {
            problemDetails.Detail = "The requested resource was not found.";
        }

        if (env.IsDevelopment())
        {
            problemDetails.Extensions["traceId"] = Activity.Current?.Id;
            problemDetails.Extensions["requestId"] = context.TraceIdentifier;
            problemDetails.Extensions["exception"] = exception.ToString();
        }

        return problemDetails;
    }

    /// <summary>
    /// Возвращает соответствующий HTTP-статус для исключения.
    /// Если исключение неизвестно, возвращает 500.
    /// </summary>
    private static HttpStatusCode GetStatusCodeForException(Exception exception)
    {
        var exceptionType = exception.GetType();

        // Проверяем, есть ли в словаре статус для текущего типа исключения
        if (ExceptionStatusCodes.TryGetValue(exceptionType, out var statusCode))
        {
            return statusCode;
        }

        // Если нет точного соответствия, проверяем базовые типы исключения (например, CustomException : ArgumentException)
        var baseExceptionType = exceptionType.BaseType;
        while (baseExceptionType != null && baseExceptionType != typeof(Exception))
        {
            if (ExceptionStatusCodes.TryGetValue(baseExceptionType, out statusCode))
            {
                return statusCode;
            }
            baseExceptionType = baseExceptionType.BaseType;
        }

        return HttpStatusCode.InternalServerError;
    }

    private string ToJson(ProblemDetails problemDetails)
    {
        try
        {
            return JsonSerializer.Serialize(problemDetails, SerializerOptions);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while serializing error to JSON.");
        }

        return "{}";
    }
}
