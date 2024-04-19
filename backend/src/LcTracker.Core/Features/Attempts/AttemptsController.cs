using LcTracker.Core.Features.Attempts.Commands;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Attempts;

public class AttemptsController(IDispatcher dispatcher, IAppDbContext dbContext) : BaseController(dispatcher)
{
    [HttpGet("api/attempts")]
    public async Task<ActionResult<GetAllAttemptsResponse>> GetAll(CancellationToken ct)
    {
        var results = await dbContext.Attempts.ToListAsync(ct);

        return Ok(new GetAllAttemptsResponse(results));
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPost("api/attempts")]
    public async Task<ActionResult> Create(CreateAttemptRequest request, CancellationToken ct)
    {
        var command = new CreateAttemptCommand(request.ProblemId, request.MinutesSpent, request.Date, request.Note,
            request.HasUsedHelp, request.HasSolved, request.IsRecap, request.Difficulty);

        await Dispatcher.DispatchAsync(command, ct);

        return Created();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("api/attempts/{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        var command = new DeleteAttemptCommand(id);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("api/attempts/{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateAttemptRequest request, CancellationToken ct)
    {
        var command = new UpdateAttemptCommand(id, request.ProblemId, request.MinutesSpent, request.Date, request.Note,
            request.HasUsedHelp, request.HasSolved, request.IsRecap, request.Difficulty);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [HttpGet("api/attempts/{id}")]
    public async Task<ActionResult<Attempt>> Get(Guid id, CancellationToken ct)
    {
        var result = await dbContext.Attempts.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (result is null)
        {
            return NotFound();
        }

        return result;
    }


    public record GetAllAttemptsResponse(IEnumerable<Attempt> Value);
}
