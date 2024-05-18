using System.Net.Mime;
using System.Text.Json;
using LcTracker.Core.Common;
using LcTracker.Core.Features.CurrentUser.Queries.Export;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Web;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Core.Features.CurrentUser;

public class CurrentUserController(IDispatcher dispatcher) : BaseController(dispatcher)
{
    [HttpGet("api/me/export")]
    public async Task<ActionResult> Export(CancellationToken ct)
    {
        var query = new ExportCurrentUserDataQuery();

        var result = await Dispatcher.QueryAsync(query, ct);

        if (!result.IsSuccess)
        {
            return result.Error.ToErrorResponse();
        }

        Response.Headers.ContentDisposition = new ContentDisposition
        {
            FileName = "exported.json",
        }.ToString();

        var ms = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(result.Value));

        return File(ms, MediaTypeNames.Application.Json);
    }
}
