using LcTracker.Core.Features.Problems;
using LcTracker.Core.Storage.EntityConfigurations;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase.Arrange;

public static class CreateFixture
{
    public static Fixture Execute()
    {
        var fixture = new Fixture();

        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

        fixture.Customizations.Add(
            new UtcConverter(
                new RandomDateTimeSequenceGenerator()));

        fixture.Customize<ProblemMethod>(c =>
            c.With(x => x.Name,
                () => fixture.Create<string>()[..ProblemEntityTypeConfiguration.MaxMethodNameLength])
        );
        return fixture;
    }
}
