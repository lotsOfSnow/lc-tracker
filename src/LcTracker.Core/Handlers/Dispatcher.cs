using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Core.Handlers;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    /// <inheritdoc />
    public Task DispatchAsync<TCommand>(TCommand request, CancellationToken ct = default)
        where TCommand : ICommand
        => serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().HandleAsync(request, ct);

    /// <inheritdoc />
    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand request, CancellationToken ct = default)
        where TCommand : ICommand<TResult>
        => serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>().HandleAsync(request, ct);
}
