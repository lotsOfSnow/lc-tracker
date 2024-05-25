using LcTracker.Core.Auth;
using LcTracker.Core.Features.Problems.Contracts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems.Commands;

public record UpdateProblemRequest(string? Note, IEnumerable<ProblemMethodDto>? Methods);

public record UpdateProblemCommand(Guid Id, string? Note, IEnumerable<ProblemMethodDto>? Methods) : ICommand;

public class UpdateProblemCommandHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<UpdateProblemCommand>
{
    public async Task<Result> Handle(UpdateProblemCommand command, CancellationToken ct)
    {
        var problem = await dbContext
            .UserProblems(getCurrentUserId)
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (problem is null)
        {
            return Errors.NotFound.Create();
        }

        if (problem.IsLocked)
        {
            return Errors.Locked.Create();
        }

        problem.Methods = ProblemMethodDto.Map(command.Methods);
        problem.Note = command.Note?.Trim();

        await dbContext.SaveChangesAsync(ct);

        return Result.Ok;
    }
}
