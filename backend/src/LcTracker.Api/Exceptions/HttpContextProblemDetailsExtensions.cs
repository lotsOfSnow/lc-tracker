using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Api.Exceptions;

public static class HttpContextProblemDetailsExtensions
{
    public static Task WriteProblemDetailsAsync(
        this HttpContext self,
        ProblemDetails problemDetails,
        CancellationToken ct = default)
    {
        self.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        return self.Response.WriteAsJsonAsync(problemDetails, ct);
    }
}
