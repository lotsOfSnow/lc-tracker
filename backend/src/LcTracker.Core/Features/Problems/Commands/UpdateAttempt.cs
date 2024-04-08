using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems.Commands;

public record UpdateProblemRequest(string Name, int Number, string Url);

public record UpdateProblemCommand(Guid Id, string Name, int Number, string Url) : ICommand;

public class UpdateProblemCommandHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<UpdateProblemCommand>
{
    public async Task HandleAsync(UpdateProblemCommand command, CancellationToken ct)
    {
        var problem = await dbContext
            .UserProblems(getCurrentUserId)
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (problem is null)
        {
            throw new Exception("No problem found");
        }

        problem.Name = command.Name;
        problem.Number = command.Number;
        problem.Url = command.Url;

        await dbContext.SaveChangesAsync(ct);
    }
}
