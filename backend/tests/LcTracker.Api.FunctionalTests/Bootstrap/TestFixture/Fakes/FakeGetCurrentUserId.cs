using System.Security.Claims;
using LcTracker.Core.Auth;
using Microsoft.AspNetCore.Http;

namespace LcTracker.Api.FunctionalTests.Bootstrap.TestFixture.Fakes;

public class FakeGetCurrentUserId(IHttpContextAccessor accessor) : IGetCurrentUserId
{
    public Guid Execute()
    {
        var claimsId = accessor
            .HttpContext!
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(claimsId, out var id))
        {
            throw new($"Couldn't parse claims id ({claimsId})");
        }

        return id;
    }
}
