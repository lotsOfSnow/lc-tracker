using LcTracker.Core.Features.Problems.Commands;
using LcTracker.Core.Handlers;
using LcTracker.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Core.Features.Problems;

public class ProblemsController(IDispatcher dispatcher) : BaseController(dispatcher)
{
    [HttpPost("api/problems")]
    public async Task<ActionResult> Create(CreateProblemRequest request, CancellationToken ct)
    {
        var command = new CreateProblemCommand(request.Name, request.Number, request.Url);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }
}
