using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Shared.Handlers;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public Task DispatchAsync<TCommand>(TCommand request, CancellationToken ct = default)
        where TCommand : ICommand
        => serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().HandleAsync(request, ct);

    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand request, CancellationToken ct = default)
        where TCommand : ICommand<TResult>
        => serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>().HandleAsync(request, ct);

    public Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken ct = default)
        where TQuery : IQuery<TResult>
        => serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>().HandleAsync(query, ct);
}
