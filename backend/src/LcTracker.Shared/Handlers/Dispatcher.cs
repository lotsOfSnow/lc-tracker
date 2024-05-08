using LcTracker.Shared.Results;
using MediatR;

namespace LcTracker.Shared.Handlers;

public class Dispatcher(ISender sender) : IDispatcher
{
    public Task<Result> DispatchAsync(ICommand request, CancellationToken ct = default)
        => sender.Send(request, ct);

    public Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> request, CancellationToken ct = default)
        => sender.Send(request, ct);

    public Task<Result<TResult>> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default)
        => sender.Send(query, ct);
}
