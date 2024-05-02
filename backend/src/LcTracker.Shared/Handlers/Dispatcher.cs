using MediatR;

namespace LcTracker.Shared.Handlers;

public class Dispatcher(ISender sender) : IDispatcher
{
    public Task DispatchAsync<TCommand>(TCommand request, CancellationToken ct = default)
        where TCommand : ICommand
        => sender.Send(request, ct);

    public Task<TResult> DispatchAsync<TCommand, TResult>(TCommand request, CancellationToken ct = default)
        where TCommand : ICommand<TResult>
        => sender.Send(request, ct);

    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default)
        => sender.Send(query, ct);
}
