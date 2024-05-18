using LcTracker.Api.FunctionalTests.Bootstrap.TestFixture;

namespace LcTracker.Api.FunctionalTests.Features;

[CollectionDefinition(Name)]
public class FeaturesCollection : ICollectionFixture<ApiTestFixture>
{
    internal const string Name = nameof(FeaturesCollection);
}
