using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace LcTracker.UnitTests.Shared;

public abstract class BaseUnitTest<T> where T : class
{
    protected IFixture Fixture { get; } = new Fixture().Customize(new AutoNSubstituteCustomization());

    protected T CreateSut() => Fixture.Create<T>();
}
