using LcTracker.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Shared.Web;

[ApiController]
public abstract class BaseController(IDispatcher dispatcher) : ControllerBase
{
    protected IDispatcher Dispatcher { get; } = dispatcher;
}
