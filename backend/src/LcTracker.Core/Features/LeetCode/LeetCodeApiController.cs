using System.Net;
using LcTracker.Core.Common;
using LcTracker.Core.Features.LeetCode.Queries;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Core.Features.LeetCode;

public class LeetCodeApiController(IDispatcher dispatcher) : BaseController(dispatcher)
{
    [HttpGet("problems")]
    public async Task<ActionResult<LeetCodeQuestion>> GetQuestion([FromQuery] string url, CancellationToken ct)
    {
        var result = await dispatcher.QueryAsync(new GetLeetCodeQuestionQuery(url), ct);

        return result.ToResponse(val => val, HttpStatusCode.OK);
    }
}
