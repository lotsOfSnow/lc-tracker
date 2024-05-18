using AutoFixture.Dsl;
using LcTracker.Shared.Entities;

namespace LcTracker.Api.FunctionalTests.TestUtilities;

public static class FixtureExtensions
{
    public static IPostprocessComposer<T> BuildWithUserId<T>(this Fixture self, Guid id)
        where T : IOwned
        => self.Build<T>().With(x => x.AppUserId, id);
}
