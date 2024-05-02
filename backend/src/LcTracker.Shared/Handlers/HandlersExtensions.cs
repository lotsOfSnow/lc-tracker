using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Handlers;

public static class HandlersExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
