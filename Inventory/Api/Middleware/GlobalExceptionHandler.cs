using Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };

        // Unauthorized (401)
        if (exception is UnauthorizedException unauthorizedException)
        {
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Title = "Unauthorized";
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1";
            problemDetails.Detail = unauthorizedException.Message;

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        // Forbidden (403)
        if (exception is ForbiddenException forbiddenException)
        {
            problemDetails.Status = StatusCodes.Status403Forbidden;
            problemDetails.Title = "Forbidden";
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3";
            problemDetails.Detail = forbiddenException.Message;

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        // Validation Error (400)
        if (exception is ValidationException validationException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Validation Error";
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            problemDetails.Detail = "One or more validation errors occurred.";
            problemDetails.Extensions["errors"] = validationException.Errors;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        // Not Found (404)
        if (exception is NotFoundException notFoundException)
        {
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Title = "Not Found";
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            problemDetails.Detail = notFoundException.Message;

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        // Invalid Operation (400)
        if (exception is InvalidOperationException invalidOperationException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Invalid Operation";
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            problemDetails.Detail = invalidOperationException.Message;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        // Argument Error (400)
        if (exception is ArgumentException argumentException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Bad Request";
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            problemDetails.Detail = argumentException.Message;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        // Default: 500 Internal Server Error
        context.Response.StatusCode = problemDetails.Status.Value;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}