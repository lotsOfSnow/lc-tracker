using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Functions;

public static class FunctionsExtensions
{
    public static IServiceCollection AddFunctions(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var functionTypes = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x is { IsClass: true, IsAbstract: false } && typeof(IFunction).IsAssignableFrom(x));

        foreach (var functionType in functionTypes)
        {
            services.AddTransient(functionType);
        }

        return services;
    }
}
