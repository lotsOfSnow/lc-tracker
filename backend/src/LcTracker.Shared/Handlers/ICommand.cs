using MediatR;

namespace LcTracker.Shared.Handlers;

public interface ICommand : IRequest;

public interface ICommand<out T> : IRequest<T>;
