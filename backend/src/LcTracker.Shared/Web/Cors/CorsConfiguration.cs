using LcTracker.Shared.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LcTracker.Shared.Web.Cors;

public static class CorsConfiguration
{
    private const string PolicyName = "AppCorsPolicy";

    public static void AddAppCors(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatedOptions<AppCorsOptions>(AppCorsOptions.SectionName);

        builder.Services.AddSingleton<IConfigureOptions<CorsOptions>>(
            sp =>
            {
                var myOptions = sp.GetRequiredService<IOptions<AppCorsOptions>>().Value;

                return new ConfigureNamedOptions<CorsOptions>(
                    Microsoft.Extensions.Options.Options.DefaultName, corsOptions =>
                    {
                        corsOptions.AddPolicy(
                            PolicyName, policyBuilder =>
                            {
                                policyBuilder
                                    .WithOrigins(myOptions.AllowedOrigin)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials()
                                    .WithExposedHeaders("Content-Disposition");
                            });
                    });
            });
    }

    public static void UseAppCors(this WebApplication app) =>
        app.UseCors(PolicyName);
}
