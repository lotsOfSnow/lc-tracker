using LcTracker.Core.Features.AppUsers;
using LcTracker.Shared.Entities;

namespace LcTracker.Api.FunctionalTests.Bootstrap.Context;

public class TestContextPrerequisiteData(
    TestContextDatabase database)
{
    public async Task<T> Need<T>(T data)
        where T : Entity
    {
        var inserted = await database.AddAsync(data);
        if (inserted == 0)
        {
            throw new NoDbChangesWrittenException();
        }

        return data;
    }

    public async Task<AppUser> NeedUser()
        => await Need(new AppUser());
}
