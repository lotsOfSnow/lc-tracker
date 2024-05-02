using System.Net.Mime;
using System.Text.Json;
using LcTracker.Core.Features.Problems.Commands;
using LcTracker.Core.Features.Problems.Queries.Export;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems;

public class ProblemsController(IDispatcher dispatcher, IAppDbContext dbContext) : BaseController(dispatcher)
{
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPost("api/problems")]
    public async Task<ActionResult> Create(CreateProblemRequest request, CancellationToken ct)
    {
        var command = new CreateProblemCommand(request.Name, request.Number, request.Url);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [HttpGet("api/problems")]
    public async Task<ActionResult<GetAllProblemsResponse>> GetAll(CancellationToken ct)
    {
        var results = await dbContext.Problems.ToListAsync(ct);

        return Ok(new GetAllProblemsResponse(results));
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [HttpGet("api/problems/{id}")]
    public async Task<ActionResult<Problem>> Get(Guid id, CancellationToken ct)
    {
        var result = await dbContext.Problems.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (result is null)
        {
            return NotFound();
        }

        return result;
    }

    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("api/problems/{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateProblemRequest request, CancellationToken ct)
    {
        var command = new UpdateProblemCommand(id, request.Name, request.Number, request.Url);

        await Dispatcher.DispatchAsync(command, ct);

        return NoContent();
    }

    [HttpGet("api/problems/export")]
    public async Task<FileResult> Export(CancellationToken ct)
    {
        var query = new ExportProblemsQuery();

        var result = await Dispatcher.QueryAsync(query, ct);

        Response.Headers.ContentDisposition = new ContentDisposition
        {
            FileName = "exported.json",
        }.ToString();

        using var ms = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(result));

        return File(ms, MediaTypeNames.Application.Json);
    }

    public record GetAllProblemsResponse(IEnumerable<Problem> Value);
}
