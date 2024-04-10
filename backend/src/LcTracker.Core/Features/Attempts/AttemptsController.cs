using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Attempts;

public class AttemptsController(IDispatcher dispatcher, IAppDbContext dbContext) : BaseController(dispatcher)
{
    [HttpGet("api/attempts")]
    public async Task<ActionResult<IEnumerable<Attempt>>> GetAll(CancellationToken ct)
    {
        var results = await dbContext.Attempts.ToListAsync(ct);

        return Ok(results);
    }

    [HttpPost("api/attempts")]
    public async Task<ActionResult> Create(CreateAttemptRequest request, CancellationToken ct)
    {
        var command = new CreateAttemptCommand(request.ProblemId, request.MinutesSpent);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [HttpDelete("api/attempts/{id:guid}")]
    public async Task<ActionResult> Create(Guid id, CancellationToken ct)
    {
        var command = new DeleteAttemptCommand(id);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [HttpPut("api/attempts/{id:guid}")]
    public async Task<ActionResult> Create(Guid id, UpdateAttemptRequest request, CancellationToken ct)
    {
        var command = new UpdateAttemptCommand(id, request.ProblemId, request.MinutesSpent);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }
}
