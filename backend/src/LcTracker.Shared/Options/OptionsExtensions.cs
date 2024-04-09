using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Options;

public static class OptionsExtensions
{
    public static IServiceCollection AddValidatedOptions<T>(
        this IServiceCollection services,
        string sectionPath)
        where T : class
    {
        services
            .AddOptionsWithValidateOnStart<T>()
            .BindConfiguration(sectionPath)
            .ValidateDataAnnotations();

        return services;
    }
}
