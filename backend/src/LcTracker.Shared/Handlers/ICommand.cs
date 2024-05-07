using LcTracker.Shared.Results;
using MediatR;

namespace LcTracker.Shared.Handlers;

public interface ICommand : IRequest<Result>;

public interface ICommand<T> : IRequest<Result<T>>;
