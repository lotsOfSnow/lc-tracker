using AutoFixture.Kernel;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestBase.Arrange;

// https://stackoverflow.com/questions/22686907/let-autofixture-create-datetime-in-utc
public class UtcConverter : ISpecimenBuilder
{
    private readonly ISpecimenBuilder _builder;

    public UtcConverter(ISpecimenBuilder builder)
    {
        _builder = builder;
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type t || t != typeof(DateTime))
        {
            return new NoSpecimen();
        }

        var specimen = this._builder.Create(request, context);
        if (specimen is not DateTime time)
        {
            return new NoSpecimen();
        }

        return time.ToUniversalTime();
    }
}
