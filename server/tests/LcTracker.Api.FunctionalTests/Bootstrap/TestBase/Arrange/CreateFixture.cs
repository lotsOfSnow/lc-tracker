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

        return fixture;
    }
}
