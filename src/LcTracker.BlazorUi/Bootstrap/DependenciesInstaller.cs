using LcTracker.BlazorUi.Components;
using LcTracker.Core.Storage;

namespace LcTracker.BlazorUi.Bootstrap;

public static class DependenciesInstaller
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.AddStorage();
    }

    public static async Task UseDependenciesAsync(this WebApplication app)
    {
        await app.UseStorageAsync();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
}
