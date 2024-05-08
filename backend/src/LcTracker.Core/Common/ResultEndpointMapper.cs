using System.Net;
using LcTracker.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Core.Common;

public static class ResultEndpointMapper
{
    public static ActionResult ToResponse(this Result result, HttpStatusCode statusCode)
    {
        if (!result.IsSuccess)
        {
            return ToErrorResponse(result.Error);
        }

        return new StatusCodeResult((int)statusCode);
    }

    public static ActionResult<TValue> ToResponse<T, TValue>(
        this Result<T> result,
        Func<TValue> or,
        HttpStatusCode statusCode)
    {
        return !result.IsSuccess
            ? ToErrorResponse(result.Error) : new(or()) { StatusCode = (int)statusCode };
    }

    public static ActionResult ToResponse<T>(
        this Result<T> result,
        HttpStatusCode statusCode)
    {
        return !result.IsSuccess ? ToErrorResponse(result.Error) : new StatusCodeResult((int)statusCode);
    }

    public static ObjectResult ToErrorResponse(this ResultError error)
    {
        var statusCode = StatusCodeMapper.ToStatusCode(error.Code);

        var detail = string.Join(',', error.Messages);

        var content = new ProblemDetails
        {
            Type = error.Code.ToString(),
            Status = (int)statusCode,
            Title = "Request failed",
            Detail = string.IsNullOrWhiteSpace(detail) ? null : detail,
        };

        return new(content) { StatusCode = (int)statusCode };
    }

    private static class StatusCodeMapper
    {
        public static HttpStatusCode ToStatusCode(ErrorCode code) => code.Value switch
        {
           Errors.NotFoundCode => HttpStatusCode.NotFound,
            _ => HttpStatusCode.BadRequest,
        };
    }
}
