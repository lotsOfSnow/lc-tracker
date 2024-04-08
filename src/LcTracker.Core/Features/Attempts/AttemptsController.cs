using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Attempts;

public class AttemptsController(IDispatcher dispatcher, IAppDbContext dbContext) : BaseController(dispatcher)
{
    [HttpPost("api/attempts")]
    public async Task<ActionResult> Create(CreateAttemptRequest request, CancellationToken ct)
    {
        var command = new CreateAttemptCommand(request.ProblemId, request.MinutesSpent);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [HttpGet("api/attempts")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var results = await dbContext.Attempts.ToListAsync(ct);

        return Ok(results);
    }
}
