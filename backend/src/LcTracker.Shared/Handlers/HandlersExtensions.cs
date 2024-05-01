using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Handlers;

public static class HandlersExtensions
{
    private static readonly List<Type> _handlerInterfaces =
        [typeof(ICommandHandler<>), typeof(ICommandHandler<,>), typeof(IQueryHandler<,>)];
    public static IServiceCollection AddHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var handlerTypes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsInterface: false, IsAbstract: false })
            .SelectMany(type => type.GetInterfaces(),
                (type, @interface) => new
                {
                    Type = type, Interface = @interface,
                })
            .Where(ti => ti.Interface.IsGenericType &&
                         _handlerInterfaces.Any(i => i == ti.Interface.GetGenericTypeDefinition()));

        foreach (var handlerType in handlerTypes)
        {
            services.AddTransient(handlerType.Interface, handlerType.Type);
        }

        return services;
    }
}
