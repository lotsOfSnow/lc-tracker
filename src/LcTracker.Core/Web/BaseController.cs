using LcTracker.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace LcTracker.Core.Web;

[ApiController]
public abstract class BaseController(IDispatcher dispatcher) : ControllerBase
{
    protected IDispatcher Dispatcher { get; } = dispatcher;
}
