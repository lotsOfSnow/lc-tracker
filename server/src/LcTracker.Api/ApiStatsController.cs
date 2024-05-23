using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Api;

public class ApiStatsController(IDispatcher dispatcher, IHostEnvironment environment) : BaseController(dispatcher)
{
    [HttpGet("/api/api-stats")]
    public ActionResult<Stats> GetStats()
    {
        var stats = new Stats(environment.EnvironmentName);

        return Ok(stats);
    }

    public record Stats(string Environment);
}
