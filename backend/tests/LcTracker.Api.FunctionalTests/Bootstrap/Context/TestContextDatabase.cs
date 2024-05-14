using LcTracker.Core.Storage;
using LcTracker.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LcTracker.Api.FunctionalTests.Bootstrap.Context;

public class TestContextDatabase(
    IServiceProvider services)
{
    public async Task<T?> GetAsync<T>(
        Guid id)
        where T : Entity
    {
        await using var scope = services.CreateAsyncScope();
        var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await database
            .Set<T>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync<T>(
        Guid id)
        where T : Entity
    {
        await using var scope = services.CreateAsyncScope();
        var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await database
            .Set<T>()
            .AnyAsync(x => x.Id == id);
    }


    public async Task<long> AddAsync<T>(
        T entity)
        where T : Entity
    {
        await using var scope = services.CreateAsyncScope();
        var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await database.AddAsync(entity);
        return await database.SaveChangesAsync();
    }

    public async Task RunAsync(Func<AppDbContext, Task> assertion)
    {
        await using var scope = services.CreateAsyncScope();
        var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await assertion(database);
    }
}
