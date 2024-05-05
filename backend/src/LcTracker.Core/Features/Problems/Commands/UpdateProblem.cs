using LcTracker.Core.Auth;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems.Commands;

public record UpdateProblemMethod(string Name, string Contents);

public record UpdateProblemRequest(string Name, int Number, string Url, IEnumerable<UpdateProblemMethod> Methods);

public record UpdateProblemCommand(Guid Id, string Name, int Number, string Url, IEnumerable<UpdateProblemMethod> Methods) : ICommand;

public class UpdateProblemCommandHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<UpdateProblemCommand>
{
    public async Task Handle(UpdateProblemCommand command, CancellationToken ct)
    {
        var problem = await dbContext
            .UserProblems(getCurrentUserId)
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        // TODO don't throw
        if (problem is null)
        {
            throw new("No problem found");
        }

        problem.Name = command.Name;
        problem.Number = command.Number;
        problem.Url = command.Url;
        problem.Methods = command.Methods.Select(x => new ProblemMethod(x.Name, x.Contents)).ToHashSet();

        await dbContext.SaveChangesAsync(ct);
    }
}
