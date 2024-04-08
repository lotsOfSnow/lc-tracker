namespace LcTracker.Shared.Handlers;

public interface IDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand;

    Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        where TCommand : ICommand<TResult>;
}
