using LcTracker.Core.Features.Problems.Commands;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems;

public class ProblemsController(IDispatcher dispatcher, IAppDbContext dbContext) : BaseController(dispatcher)
{
    [HttpPost("api/problems")]
    public async Task<ActionResult> Create(CreateProblemRequest request, CancellationToken ct)
    {
        var command = new CreateProblemCommand(request.Name, request.Number, request.Url);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [HttpGet("api/problems")]
    public async Task<ActionResult<IEnumerable<Problem>>> GetAll(CancellationToken ct)
    {
        var results = await dbContext.Problems.ToListAsync(ct);

        return Ok(results);
    }

    [HttpPut("api/problems/{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateProblemRequest request, CancellationToken ct)
    {
        var command = new UpdateProblemCommand(id, request.Name, request.Number, request.Url);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }
}
