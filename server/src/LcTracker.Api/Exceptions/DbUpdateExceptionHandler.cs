using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Api.Exceptions;

public class DbUpdateExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DbUpdateException dbUpdateException)
        {
            return false;
        }

        var message = dbUpdateException switch
        {
            UniqueConstraintException e => $"Unique constraint violated: '{e.InnerException?.Message}'",
            _ => $"Error on data saving: ({dbUpdateException.GetType()}) {exception.InnerException?.Message}",
        };

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Database error",
            Detail = message,
        };

        await httpContext.WriteProblemDetailsAsync(problemDetails, cancellationToken);

        return true;
    }
}
