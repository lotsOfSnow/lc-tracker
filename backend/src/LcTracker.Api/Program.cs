using LcTracker.Api.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

builder.AddDependencies();

var app = builder.Build();

await app.UseDependenciesAsync();

app.Run();
