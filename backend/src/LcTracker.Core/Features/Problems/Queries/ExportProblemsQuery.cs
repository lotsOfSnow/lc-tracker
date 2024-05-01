using LcTracker.Core.Auth;
using LcTracker.Core.Features.Export;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;

namespace LcTracker.Core.Features.Problems.Queries;

public record ExportProblemsQuery : ICommand;

public class ExportProblemsQueryHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<ExportProblemsQuery>
{
    public async Task HandleAsync(ExportProblemsQuery command, CancellationToken ct)
    {
        await new ExportService(dbContext).Export(getCurrentUserId.Execute());
    }
}
