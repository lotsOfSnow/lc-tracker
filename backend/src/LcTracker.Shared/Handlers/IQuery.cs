using MediatR;

namespace LcTracker.Shared.Handlers;

public interface IQuery<out T> : IRequest<T>;
