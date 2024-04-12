namespace LcTracker.Api.Exceptions;

public static class ExceptionHandlersInstaller
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddExceptionHandler<DbUpdateExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
