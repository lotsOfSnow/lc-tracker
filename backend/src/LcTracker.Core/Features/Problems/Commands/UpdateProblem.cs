using LcTracker.Core.Auth;
using LcTracker.Core.Features.Problems.Contracts;
using LcTracker.Core.Storage;
using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace LcTracker.Core.Features.Problems.Commands;

public record UpdateProblemRequest(IEnumerable<ProblemMethodDto> Methods);

public record UpdateProblemCommand(Guid Id, IEnumerable<ProblemMethodDto> Methods) : ICommand;

public class UpdateProblemCommandHandler(IAppDbContext dbContext, IGetCurrentUserId getCurrentUserId) : ICommandHandler<UpdateProblemCommand>
{
    public async Task<Result> Handle(UpdateProblemCommand command, CancellationToken ct)
    {
        var problem = await dbContext
            .UserProblems(getCurrentUserId)
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (problem is null)
        {
            return Errors.NotFound;
        }

        problem.Methods = ProblemMethodDto.Map(command.Methods);

        await dbContext.SaveChangesAsync(ct);

        return Result.Ok;
    }
}
