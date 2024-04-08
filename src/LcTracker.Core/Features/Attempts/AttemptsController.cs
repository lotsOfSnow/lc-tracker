using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Core.Features.Attempts;

public class AttemptsController(IDispatcher dispatcher) : BaseController(dispatcher)
{
    [HttpPost("api/attempts")]
    public async Task<ActionResult> Create(CreateAttemptRequest request, CancellationToken ct)
    {
        var command = new CreateAttemptCommand(request.ProblemId, request.MinutesSpent);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }
}
