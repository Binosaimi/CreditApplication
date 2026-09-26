using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace credit.customers.Exceptions;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (status, title, detail) = exception switch
        {
            DbUpdateException
            {
                InnerException: PostgresException
                {
                    SqlState: PostgresErrorCodes.UniqueViolation
                }
            } => (
                StatusCodes.Status409Conflict,
                "Conflict",
                "A record with the same unique value already exists."
            ),

            HttpRequestException { StatusCode: HttpStatusCode.InternalServerError } => (
                StatusCodes.Status502BadGateway,
                "Upstream Service Error",
                "Loan service returned an internal server error."
            ),

            HttpRequestException => (
                StatusCodes.Status503ServiceUnavailable,
                "Service Unavailable",
                "A dependent service is unavailable."
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred."
            ),
        };

        logger.LogError(
            exception,
            "Request failed with status {StatusCode}",
            status);

        context.Response.StatusCode = status;

        await context.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path,
                Extensions =
                {
                    ["traceId"] = context.TraceIdentifier
                }
            },
            cancellationToken);

        return true;
    }
    
    public class NoLoansException(string message)
        : Exception(message);
    public class LitigationException(string message)
        : Exception(message);
    
    public class CustomerNotFoundException(string message)
        : Exception(message);
}