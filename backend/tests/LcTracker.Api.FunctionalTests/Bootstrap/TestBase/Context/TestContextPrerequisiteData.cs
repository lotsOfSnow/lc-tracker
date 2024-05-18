using LcTracker.Core.Features.AppUsers;
using LcTracker.Shared.Entities;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase.Context;

public class TestContextPrerequisiteData(
    TestContextDatabase database)
{
    public async Task<T> Object<T>(T data)
        where T : Entity
    {
        var inserted = await database.AddAsync(data);
        if (inserted == 0)
        {
            throw new NoDbChangesWrittenException();
        }

        return data;
    }

    public async Task<AppUser> User()
        => await Object(new AppUser());

}

public class NoDbChangesWrittenException()
    : Exception("No entries were written to the database");
