using LcTracker.Shared.Results;
using MediatR;

namespace LcTracker.Shared.Handlers;

public interface IQuery<T> : IRequest<Result<T>>;
