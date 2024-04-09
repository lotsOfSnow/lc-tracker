using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Handlers;

public static class HandlersExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var handlerTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsInterface: false, IsAbstract: false })
            .SelectMany(type => type.GetInterfaces(),
                (type, @interface) => new
                {
                    Type = type, Interface = @interface
                })
            .Where(ti => ti.Interface.IsGenericType &&
                         (ti.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                          ti.Interface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)));

        foreach (var handlerType in handlerTypes)
        {
            services.AddTransient(handlerType.Interface, handlerType.Type);
        }

        return services;
    }
}
