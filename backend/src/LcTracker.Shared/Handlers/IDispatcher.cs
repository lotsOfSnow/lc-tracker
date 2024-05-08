using LcTracker.Shared.Results;

namespace LcTracker.Shared.Handlers;

public interface IDispatcher
{
    Task<Result> DispatchAsync(ICommand command, CancellationToken ct = default);

    Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken ct = default);

    Task<Result<TResult>> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken ct = default);
}
