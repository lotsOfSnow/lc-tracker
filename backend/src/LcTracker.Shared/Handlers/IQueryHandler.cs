using LcTracker.Shared.Results;
using MediatR;

namespace LcTracker.Shared.Handlers;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}
